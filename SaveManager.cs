using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameSaveManager
{
    /// <summary>
    /// Manages a save file for you.
    /// </summary>
    public abstract class SaveManager
    {
        protected string folderName, fileName;

        /// <summary>
        /// Access save game data.
        /// </summary>
        public SaveData Data { get; set; }

        /// <summary>
        /// Creates a new save game manager.
        /// </summary>
        /// <param name="folderName">Name of the folder containing the save.</param>
        /// <param name="fileName">Name of the save file.</param>
        public SaveManager(string folderName, string fileName)
        {
            this.folderName = folderName;
            this.fileName = fileName;
            this.Data = new SaveData();
        }

        /// <summary>
        /// Loads the data from disk to memory.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Saves the data in memory to disk.
        /// </summary>
        public abstract void Save();
    }
}
