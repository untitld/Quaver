using System;
using Microsoft.Xna.Framework;
using Quaver.Shared.Assets;
using Quaver.Shared.Helpers;
using Wobble;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.Sprites.Text;
using Wobble.Managers;

namespace Quaver.Shared.Graphics.Menu.Border
{
    public class DrawableSessionTime : Sprite, IMenuBorderItem
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool UseCustomPaddingY { get; } = true;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public int CustomPaddingY { get; } = 0;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool UseCustomPaddingX { get; } = true;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public int CustomPaddingX { get; } = 25;

        /// <summary>
        ///     The time that the game has been running for
        /// </summary>
        public SpriteTextPlus Time { get; }

        /// <summary>
        ///     The time in the previous frame
        /// </summary>
        private double TimeSinceLastSecond { get; set; }

        /// <summary>
        ///     The original timespan that the clock started at
        /// </summary>
        private TimeSpan Clock { get; set; }

        /// <summary>
        /// </summary>
        public DrawableSessionTime()
        {
            Size = new ScalableVector2(114, 30);
            Image = UserInterface.SessionTimeBackground;

            Clock = TimeSpan.FromMilliseconds(GameBase.Game.TimeRunning);

            Time = new SpriteTextPlus(FontManager.GetWobbleFont(Fonts.LatoHeavy), $"{Clock.Hours:00}:{Clock.Minutes:00}:{Clock.Seconds:00}", 22)
            {
                Parent = this,
                Alignment = Alignment.MidCenter
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ChangeTime();
            base.Update(gameTime);
        }

        /// <summary>
        ///     Changes the time if a second has passed
        /// </summary>
        private void ChangeTime()
        {
            TimeSinceLastSecond += GameBase.Game.TimeSinceLastFrame;

            if (!(TimeSinceLastSecond >= 1000))
                return;

            Clock = Clock.Add(TimeSpan.FromSeconds(1));

            Time.Text = $"{Clock.Hours:00}:{Clock.Minutes:00}:{Clock.Seconds:00}";
            TimeSinceLastSecond = 0;
        }
    }
}