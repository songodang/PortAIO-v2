using EloBuddy; 
using LeagueSharp.Common; 
namespace ReformedAIO.Champions.Xerath.Drawings.Spells
{
    using System;
    using System.Drawing;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ReformedAIO.Champions.Xerath.Core.Spells;

    using RethoughtLib.FeatureSystem.Abstract_Classes;

    internal sealed class QDrawing : ChildBase
    {
        public override string Name { get; set; } = "Q";

        private readonly QSpell spell;

        public QDrawing(QSpell spell)
        {
            this.spell = spell;
        }

        public void OnEndScene(EventArgs args)
        {
            if (ObjectManager.Player.IsDead)
            {
                return;
            }

            Render.Circle.DrawCircle(ObjectManager.Player.Position,
                                     spell.Spell.Range,
                                     spell.Spell.IsReady()
                                     ? Color.Cyan
                                     : Color.DarkSlateGray,
                                     Menu.Item("Xerath.Draw.Q.Width").GetValue<Slider>().Value,
                                     Menu.Item("Xerath.Draw.Q.Z").GetValue<bool>());
        }


        protected override void OnDisable(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnDisable(sender, eventArgs);

            Drawing.OnEndScene -= OnEndScene;
        }

        protected override void OnEnable(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnEnable(sender, eventArgs);


            Drawing.OnEndScene += OnEndScene;
        }

        protected override void OnLoad(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnLoad(sender, eventArgs);

            Menu.AddItem(new MenuItem("Xerath.Draw.Q.Z", "Draw Z").SetValue(false));

            Menu.AddItem(new MenuItem("Xerath.Draw.Q.Width", "Thickness").SetValue(new Slider(3, 1, 5)));
        }
    }
}
