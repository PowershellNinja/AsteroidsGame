using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using AsteroidsGame.Buttons;
using AsteroidsGame.GameAssets;

namespace AsteroidsGame.Screens
{
    class UpgradeMenuScreen
    {

        public SpriteBatch spriteBatch;
        public Vector2 backgroundScale = new Vector2(0.64f, 0.6f);
        public Vector2 backgroundPosition;
        public Texture2D backgroundSprite;
        public StartButton startButton;        
        public ExitButton exitButton;


        public UpgradeMenuScreen(){

        }


        public void LoadUpgradeMenu(GraphicsDevice graphicsDevice, ContentManager content, int screenWidth, Texture2D upgradeMenuBackgroundSprite, Texture2D startButtonSprite, Texture2D exitButtonSprite)
        {

            //Load Background
            backgroundSprite = upgradeMenuBackgroundSprite;
            backgroundPosition = new Vector2(0, 0);

            //Load Buttons

            startButton = new StartButton(startButtonSprite, screenWidth, graphicsDevice);            
            exitButton = new ExitButton(exitButtonSprite, screenWidth, graphicsDevice);

        }

        public void DrawFrame()
        {

            this.spriteBatch.Begin();
            spriteBatch.Draw(backgroundSprite, backgroundPosition, scale: backgroundScale);
            this.spriteBatch.End();

            startButton.DrawButton();            
            exitButton.DrawButton();

        }

        public void UpdateUpgradeMenu(MouseState mouseState)
        {
            startButton.UpdateButton(mouseState);            
            exitButton.UpdateButton(mouseState);

        }


    }

//    using System;
//using Microsoft.Xna.Framework.Storage;
//using System.Xml.Serialization;
//using System.IO;

//namespace Microsoft.Xna.Samples.Storage
//    {
//        [Serializable]
//        public struct SaveGame
//        {
//            public string Name;
//            public int HiScore;
//            public DateTime Date;

//            [NonSerialized]
//            public int DontKeep;
//        }

//        public class SaveGameStorage
//        {
//            public void Save(SaveGame sg)
//            {
//                StorageDevice device = StorageDevice.ShowStorageDeviceGuide();

//                // Open a storage container
//                StorageContainer container = device.OpenContainer("TestStorage");

//                // Get the path of the save game
//                string filename = Path.Combine(container.Path, "savegame.xml");

//                // Open the file, creating it if necessary
//                FileStream stream = File.Open(filename, FileMode.OpenOrCreate);

//                // Convert the object to XML data and put it in the stream
//                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
//                serializer.Serialize(stream, sg);

//                // Close the file
//                stream.Close();

//                // Dispose the container, to commit changes
//                container.Dispose();
//            }

//            public SaveGame Load()
//            {
//                SaveGame ret = new SaveGame();

//                StorageDevice device = StorageDevice.ShowStorageDeviceGuide();

//                // Open a storage container
//                StorageContainer container = device.OpenContainer("TestStorage");

//                // Get the path of the save game
//                string filename = Path.Combine(container.Path, "savegame.xml");

//                // Check to see if the save exists
//                if (!File.Exists(filename))
//                    // Notify the user there is no save           
//                    return ret;

//                // Open the file
//                FileStream stream = File.Open(filename, FileMode.OpenOrCreate,
//                    FileAccess.Read);

//                // Read the data from the file
//                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
//                ret = (SaveGame)serializer.Deserialize(stream);

//                // Close the file
//                stream.Close();

//                // Dispose the container
//                container.Dispose();

//                return ret;
//            }
//        }
//    }

}