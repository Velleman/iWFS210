﻿using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using Foundation;

namespace WFS210.UI
{
	public class Marker
	{
		public MarkerLayout Layout { get; set; }

		public UIImage Image { get; set; }

		public CALayer Layer { get; set;}

		public string Name { get; set; }

		protected int value;

		public int Inlay { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="WFS210.UI.Marker"/> class.
		/// </summary>
		/// <param name="resourceUrl">Resource URL.</param>
		/// <param name="name">Name.</param>
		/// <param name="layout">Layout.</param>
		/// <param name="inlay">Inlay.</param>
		public Marker (string resourceUrl, string name, MarkerLayout layout, int inlay)
		{
			this.Layout = layout;

			Image = UIImage.FromBundle (resourceUrl);

			Layer = new CALayer ();
			Layer.Contents = Image.CGImage;
			var scale = Image.CurrentScale;
			Console.WriteLine (scale);
			Layer.Bounds = new CGRect (0, 0, Image.CGImage.Width/scale, Image.CGImage.Height/scale);

			Name = name;

			Inlay = inlay;

		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value
		{
			get{ return value; }
			set{ 
				this.value = value;
				if (this.Layout == MarkerLayout.Horizontal)
					Position = new CGPoint (Layer.Position.X, value);
				else
					Position = new CGPoint (value, Layer.Position.Y);
			}
		}

		/// <summary>
		/// Sets the position.
		/// </summary>
		/// <value>The position.</value>
		public CGPoint Position
		{
			set{ 
				CATransaction.Begin ();
				Layer.Position = value;
				Layer.RemoveAnimation ("position");
				CATransaction.Commit ();
			}
		}
	}
}

