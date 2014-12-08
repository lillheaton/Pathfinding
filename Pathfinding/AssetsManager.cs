using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pathfinding
{
    public class AssetsManager
    {
        private ContentManager _contentManager;
        public Dictionary<string, SpriteFont> FontDictionary { get; set; }

        public AssetsManager(ContentManager contentManager)
        {
            this._contentManager = contentManager;
            this.Init();
        }

        private void Init()
        {
            this.FontDictionary = new Dictionary<string, SpriteFont>();
            this.FontDictionary.Add("MyFont", _contentManager.Load<SpriteFont>("Fonts/MyFont"));
        }
    }
}