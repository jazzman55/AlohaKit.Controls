﻿namespace AlohaKit.Controls
{
    public class ProgressRadial : GraphicsView
    {
        private ProgressRadialDrawable ProgressRadialDrawable { get; set; }
        private bool IsInitialized = false;
        public ProgressRadial()
        {
            HeightRequest = 150;
            WidthRequest = 150;
            Opacity = 0;
            Drawable = ProgressRadialDrawable = new ProgressRadialDrawable();
            Loaded += BarChartView_Loaded;
        }

        private void BarChartView_Loaded(object sender, EventArgs e)
        {
            ProgressRadialDrawable.IsAnimating = true;
            IsInitialized = true;
            this.FadeTo(1, 1000, Easing.SinIn);
            AnimateProgress(Value);
        }


        void AnimateProgress(int progress)
        {
            var animation = new Animation(v =>
            {
                var minimumDegree = 0;
                var maximumDegree = 270;
                var differenceDegree = maximumDegree - minimumDegree;
                var difference = Maximum - Minimum;
                var progressStep = (float)differenceDegree / difference;

                var tmpEnd = (int)v * progressStep;
                ProgressRadialDrawable.ProgressAngle = tmpEnd >= maximumDegree ? 269.99f : tmpEnd;
                ProgressRadialDrawable.ProgressText = String.Format(ValueFormatString, (int)v);
                Invalidate();

                ProgressRadialDrawable.IsAnimating = false;
            }, 0, progress, easing: Easing.SinInOut);
            animation.Commit(this, "ProgressAngle", length: (uint)250);
        }

        public new static readonly BindableProperty BackgroundColorProperty =
          BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ProgressRadial), Colors.White,
              propertyChanged: (bindableObject, oldValue, newValue) =>
              {
                  if (newValue != null && bindableObject is ProgressRadial progressRadial)
                  {
                      progressRadial.UpdateBackgroundColor();
                  }
              });

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty StrokeColorProperty =
          BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(ProgressRadial), Colors.LightGray,
              propertyChanged: (bindableObject, oldValue, newValue) =>
              {
                  if (newValue != null && bindableObject is ProgressRadial progressRadial)
                  {
                      progressRadial.UpdateStrokeColor();
                  }
              });

        public Color StrokeColor
        {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(ProgressRadial), Colors.Blue,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressRadial progressRadial)
                    {
                        progressRadial.UpdateProgressColor();
                    }
                });

        public Color ProgressColor
        {
            get => (Color)GetValue(ProgressColorProperty);
            set => SetValue(ProgressColorProperty, value);
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ProgressRadial), Colors.Black, BindingMode.TwoWay,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressRadial progressRadial)
                    {
                        progressRadial.UpdateTextColor();
                    }
                });

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ProgressRadial), 24.0d, BindingMode.TwoWay,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressRadial progressRadial)
                    {
                        progressRadial.UpdateFontSize();
                    }
                });

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty DirectionProperty =
            BindableProperty.Create(nameof(Direction), typeof(ProgressRadialDirection), typeof(ProgressRadial), ProgressRadialDirection.RightToLeft,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressRadial progressRadial)
                    {
                        progressRadial.UpdateDirection();
                    }
                });

        public ProgressRadialDirection Direction
        {
            get => (ProgressRadialDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public static readonly BindableProperty MinimumProperty =
           BindableProperty.Create(nameof(Minimum), typeof(int), typeof(ProgressRadial), 0);

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly BindableProperty MaximumProperty =
           BindableProperty.Create(nameof(Maximum), typeof(int), typeof(ProgressRadial), 100);

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(int), typeof(ProgressRadial), 0, BindingMode.TwoWay,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressRadial progressRadial)
                    {
                        progressRadial.UpdateValue();
                        progressRadial.ValueChanged?.Invoke(progressRadial, new ValueChangedEventArgs((double)oldValue, (double)newValue));
                    }
                });

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty AnimateProperty =
	        BindableProperty.Create(nameof(Animate), typeof(bool), typeof(ProgressRadial), true);

        public bool Animate
        {
	        get => (bool)GetValue(AnimateProperty);
	        set => SetValue(AnimateProperty, value);
        }

        public static readonly BindableProperty ValueFormatStringProperty =
	        BindableProperty.Create(nameof(ValueFormatString), typeof(string), typeof(ProgressRadial), "{0}",
		        propertyChanged: (bindableObject, oldValue, newValue) =>
		        {
			        if (newValue != null && bindableObject is ProgressRadial progressRadial)
			        {
				        progressRadial.UpdateValue();
			        }
		        });

        public string ValueFormatString
		{
	        get => (string)GetValue(ValueFormatStringProperty);
	        set => SetValue(ValueFormatStringProperty, value);
        }

		public event EventHandler<ValueChangedEventArgs> ValueChanged;

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
                UpdateBackgroundColor();
                UpdateStrokeColor();
                UpdateProgressColor();
                UpdateTextColor();
                UpdateFontSize();
                UpdateDirection();
                UpdateValue();
            }
        }

        void UpdateBackgroundColor()
        {
            if (ProgressRadialDrawable == null)
                return;

            ProgressRadialDrawable.BackgroundColor = BackgroundColor;
            Invalidate();
        }

        void UpdateStrokeColor()
        {
            if (ProgressRadialDrawable == null)
                return;

            ProgressRadialDrawable.StrokeColor = StrokeColor;
            Invalidate();
        }

        void UpdateProgressColor()
        {
            if (ProgressRadialDrawable == null)
                return;

            ProgressRadialDrawable.ProgressColor = ProgressColor;
            Invalidate();
        }

        void UpdateTextColor()
        {
            if (ProgressRadialDrawable == null)
                return;

            ProgressRadialDrawable.TextColor = TextColor;
            Invalidate();
        }

        void UpdateFontSize()
        {
            if (ProgressRadialDrawable == null)
                return;

            ProgressRadialDrawable.FontSize = FontSize;
            Invalidate();
        }

        void UpdateDirection()
        {
            if (ProgressRadialDrawable == null)
                return;

            ProgressRadialDrawable.Direction = Direction;
            Invalidate();
        }

        void UpdateValue()
        {
            if (ProgressRadialDrawable == null)
                return;

            var minimumDegree = 0;
            var maximumDegree = 270;
            var differenceDegree = maximumDegree - minimumDegree;

            var difference = Maximum - Minimum;

            var progressStep = differenceDegree / difference;

            ProgressRadialDrawable.ProgressAngle = Value * progressStep;
            ProgressRadialDrawable.ProgressText = String.Format(ValueFormatString, Value);

            if (!Animate)
            {
				Invalidate();
	            return;
            }

            if (!ProgressRadialDrawable.IsAnimating && IsInitialized)
                AnimateProgress(Value);
        }
    }
}