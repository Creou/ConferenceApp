using System;
using System.ComponentModel;
using Creou.ConferenceApp.XamarinClient.WinPhone.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Microsoft.Phone.Controls;
using XamarinRating = Creou.ConferenceApp.XamarinClient.CustomControls.Rating;

[assembly: ExportRenderer(typeof(XamarinRating), typeof(RatingRenderer))]

namespace Creou.ConferenceApp.XamarinClient.WinPhone.CustomRenderers
{
	public class RatingRenderer : ViewRenderer<XamarinRating, Rating>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<XamarinRating> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				var wpRatingControl = new Rating();
				wpRatingControl.ValueChanged += wpRating_ValueChanged;
				SetNativeControl(wpRatingControl);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control != null && Element != null)
			{
				if (e.PropertyName == XamarinRating.ValueProperty.PropertyName && Element.Value != Control.Value)
				{
					Control.Value = Element.Value;
				}
			}
		}

		protected void wpRating_ValueChanged(object sender, EventArgs e)
		{
			if (Element.Value != Control.Value)
			{
				Element.Value = Control.Value;
			}
		}
	}
}
