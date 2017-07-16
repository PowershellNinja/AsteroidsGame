using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AsteroidsGame
{
    public static class PngLoader
    {
        public static Texture2D Load(GraphicsDevice device, string path)
        {
            // TODO: Dispose stream
            var stream = File.OpenRead(path);
            return Texture2D.FromStream(device, stream);
        }
    }
}
