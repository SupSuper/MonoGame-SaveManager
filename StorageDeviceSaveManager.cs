using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MonoGameSaveManager
{
    /// <summary>
    /// Uses the XNA StorageDevice to load/save game data.
    /// Saves end up on "C:\Users\...\Documents\SavedGames\..." on Windows. 
    /// </summary>
    public class StorageDeviceSaveManager : SaveManager
    {
        private PlayerIndex? player;

        /// <summary>
        /// Creates a new save game manager based on XNA StorageDevice.
        /// </summary>
        /// <param name="folderName">Name of the folder containing the save.</param>
        /// <param name="fileName">Name of the save file.</param>
        /// <param name="player">Player the save belongs to, or null for a global save.</param>
        public StorageDeviceSaveManager(string folderName, string fileName, PlayerIndex? player)
            : base(folderName, fileName)
        {
            this.player = player;
        }

        private StorageDevice getStorageDevice()
        {
            IAsyncResult result;
            // Get a global folder.
            if (player == null)
                result = StorageDevice.BeginShowSelector(null, null);
            // Get a player-specific subfolder.
            else
                result = StorageDevice.BeginShowSelector((PlayerIndex)player, null, null);

            result.AsyncWaitHandle.WaitOne();
            StorageDevice device = StorageDevice.EndShowSelector(result);
            result.AsyncWaitHandle.Close();
            return device;
        }

        public override void Load()
        {
            // Open a storage device.
            StorageDevice device = getStorageDevice();

            // Open a storage container.
            IAsyncResult result = device.BeginOpenContainer(folderName, null, null);
            result.AsyncWaitHandle.WaitOne();
            using (StorageContainer container = device.EndOpenContainer(result))
            {
                result.AsyncWaitHandle.Close();

                // Ignore if save doesn't exist.
                if (!container.FileExists(fileName))
                    return;

                // Open the save file.
                using (Stream stream = container.OpenFile(fileName, FileMode.Open))
                {
                    // Get the XML data from the stream and convert it to object.
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    Data = (SaveData)serializer.Deserialize(stream);
                }
            }
        }

        public override void Save()
        {
            // Open a storage device.
            StorageDevice device = getStorageDevice();

            // Open a storage container.
            IAsyncResult resultStorage = device.BeginOpenContainer(folderName, null, null);
            resultStorage.AsyncWaitHandle.WaitOne();
            using (StorageContainer container = device.EndOpenContainer(resultStorage))
            {
                resultStorage.AsyncWaitHandle.Close();

                // Delete old save file.
                if (container.FileExists(fileName))
                    container.DeleteFile(fileName);

                // Create new save file.
                using (Stream stream = container.CreateFile(fileName))
                {
                    // Convert the object to XML data and put it in the stream.
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    serializer.Serialize(stream, Data);
                }
            }
        }
    }
}
