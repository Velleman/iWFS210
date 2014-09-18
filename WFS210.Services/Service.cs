﻿using System;

namespace WFS210.Services
{
	public delegate void SettingsDelegate ();

	public abstract class Service
	{
		private readonly Oscilloscope oscilloscope;

		/// <summary>
		/// Gets the oscilloscope associated with this service.
		/// </summary>
		/// <value>The oscilloscope.</value>
		public Oscilloscope Oscilloscope {
			get { return oscilloscope; }
		}

		/// <summary>
		/// Occurs when settings have changed.
		/// </summary>
		public event EventHandler SettingsChanged;

		/// <summary>
		/// Raises the settings changed event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		public void OnSettingsChanged(EventArgs e)
		{
			if (SettingsChanged != null) 
			{
				SettingsChanged (this, e);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WFS210.Services.Service"/> class.
		/// </summary>
		/// <param name="oscilloscope">Oscilloscope.</param>
		protected Service (Oscilloscope oscilloscope)
		{
			this.oscilloscope = oscilloscope;
		}

		/// <summary>
		/// Applies the settings.
		/// </summary>
		public abstract void ApplySettings ();

		/// <summary>
		/// Requests the settings.
		/// </summary>
		public abstract void RequestSettings ();

		/// <summary>
		/// Update this instance. This function should be called regularly.
		/// </summary>
		public abstract void Update ();

		public void Execute(ServiceCommand command)
		{
			command.Execute (this);
			ApplySettings ();
		}
	}
}
