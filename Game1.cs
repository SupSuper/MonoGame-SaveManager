#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
#endregion

namespace MonoGameSaveManager
{
    /// <summary>
    /// A basic game to test out the SaveManager functionality.
    /// Just follow along with the comments.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SaveManager save;
        bool saved = false;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            string saveFolder = "SaveManagerTest"; // put your save folder name here
            string saveFile = "test.sav"; // put your save file name here

            // Pick one:

            // - this will use the XNA StorageDevice method.
            save = new StorageDeviceSaveManager(saveFolder, saveFile, PlayerIndex.One);

            // - this will use the .NET IsolatedStorage method.
            //save = new IsolatedStorageSaveManager(saveFolder, saveFile);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // test save stuff
            // since this is Update, i don't want it to keep saving every tick
            if (!saved)
            {
                saved = true;

                // let's make up some save data
                save.Data.testInt = 434;
                save.Data.testBool = false;
                save.Data.testString = "wow a test";

                // save it
                save.Save();

                // erase data so we can check if loading works
                save.Data = new SaveData();

                // load it back
                save.Load();

                // did it work? let's hope so!
                // this will show up in Visual Studio's Output window when running.
                // you can also just use a Breakpoint or whatever.
                Debug.WriteLine("final save data:");
                Debug.WriteLine("testInt = " + save.Data.testInt);
                Debug.WriteLine("testBool = " + save.Data.testBool);
                Debug.WriteLine("testString = " + save.Data.testString);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
