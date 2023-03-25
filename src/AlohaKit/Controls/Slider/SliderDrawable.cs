using AlohaKit.Extensions;

namespace AlohaKit.Controls
{
    public class SliderDrawable : IDrawable
	{
		const int cornerRadius = 5;
		public Paint BackgroundPaint { get; set; }
		public double Minimum { get; set; }
		public double Maximum { get; set; }
		public double Value { get; set; }
		public Paint MinimumPaint { get; set; }
		public Paint MaximumPaint { get; set; }
		public Paint ThumbPaint { get; set; }
        public ThumbShape ThumbShape { get; set; }

        public int ScaleSteps { get; set; }
        public Paint ScalePaint { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			DrawBackground(canvas, dirtyRect);
			DrawTrackBackground(canvas, dirtyRect);
			DrawTrackProgress(canvas, dirtyRect);
			DrawThumb(canvas, dirtyRect);
		}

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			if (BackgroundPaint != null)
			{
				canvas.SetFillPaint(BackgroundPaint, dirtyRect);

				canvas.FillRectangle(dirtyRect);
			}

			if (ScaleSteps > 0)
			{
				canvas.SetFillPaint(ScalePaint, dirtyRect);
				var width = 2;
				var height = 20;

				var y = (float)((dirtyRect.Height - height) / 2);

				var d = dirtyRect.Width / (ScaleSteps + 1);

				for (int i = 1; i <= ScaleSteps; i++)
				{
					var x = dirtyRect.X + d * i;
					canvas.FillRoundedRectangle(x, y, width, height, 0);
				}



			}

			canvas.RestoreState();
		}

		public virtual void DrawTrackBackground(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			if(MaximumPaint != null)
				canvas.SetFillPaint(MaximumPaint, dirtyRect);

			var x = dirtyRect.X;

			var width = dirtyRect.Width;
			var height = 2;

			var y = (float)((dirtyRect.Height - height) / 2);

			canvas.FillRoundedRectangle(x, y, width, height, 0);

			canvas.RestoreState();
		}

		public virtual void DrawTrackProgress(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			if (MinimumPaint != null)
				canvas.SetFillPaint(MinimumPaint, dirtyRect);

			var x = dirtyRect.X;

			var value = (Value / Maximum - Minimum).Clamp(0, 1);
			var width = (float)(dirtyRect.Width * value);

			const float TrackSize = 2f;

			var height = TrackSize;

			var y = (float)((dirtyRect.Height - height) / 2);

			canvas.FillRoundedRectangle(x, y, width, height, 0);

			canvas.RestoreState();
		}

		public virtual void DrawThumb(ICanvas canvas, RectF dirtyRect)
		{
			const float ThumbSize = 18f;

			canvas.SaveState();

			var value = (Value / Maximum - Minimum).Clamp(0, 1);
			var x = (float)((dirtyRect.Width * value) - (ThumbSize / 2));

			if (x <= 0)
				x = 0;

			if (x >= dirtyRect.Width - ThumbSize)
				x = dirtyRect.Width - ThumbSize;

			var y = (float)((dirtyRect.Height - ThumbSize) / 2);
		
			if (ThumbPaint != null)
				canvas.SetFillPaint(ThumbPaint, dirtyRect);

            switch (ThumbShape)
            {
                case ThumbShape.Circle:
                    canvas.FillEllipse(x, y, ThumbSize, ThumbSize);
                    break;
                case ThumbShape.Rectangle:
					canvas.FillRectangle(x, y, ThumbSize, ThumbSize);
                    break;
                case ThumbShape.RoundedRectangle:
					canvas.FillRoundedRectangle(x, y, ThumbSize, ThumbSize, cornerRadius);
                    break;                
            }            

			canvas.RestoreState();
		}        
	}
}