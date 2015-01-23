using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Creou.ConferenceApp.XamarinClient.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinRating = Creou.ConferenceApp.XamarinClient.CustomControls.Rating;

[assembly: ExportRenderer(typeof(XamarinRating), typeof(RatingRenderer))]

namespace Creou.ConferenceApp.XamarinClient.Droid.CustomRenderers
{
	public class RatingRenderer : ViewRenderer<XamarinRating, RatingBar>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<XamarinRating> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null && Element != null)
			{
				var AndroidRatingControl = new RatingBar(Forms.Context);
				AndroidRatingControl.RatingBarChange += androidRating_ValueChanged;

				AndroidRatingControl.NumStars = 5;
				//AndroidRatingControl.Width = Wrap_Content

				SetNativeControl(AndroidRatingControl);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control != null && Element != null)
			{
				if (e.PropertyName == XamarinRating.ValueProperty.PropertyName && Element.Value != Control.Rating)
				{	
					Control.Rating = (float)Element.Value;
				}
			}
		}

		protected void androidRating_ValueChanged(object sender, EventArgs e)
		{
			if (Element.Value != Control.Rating)
			{
				Element.Value = Control.Rating;
			}
		}
	}
}