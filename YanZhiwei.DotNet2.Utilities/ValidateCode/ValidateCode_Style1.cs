﻿using System;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace YanZhiwei.DotNet2.Utilities.ValidateCode
{
    /// <summary>
    /// 线条干扰(蓝色)
    /// </summary>
    public class ValidateCode_Style1 : ValidateCodeType
    {
        private Color backgroundColor = Color.White;
        private bool chaos = true;
        private Color chaosColor = Color.FromArgb(170, 170, 0x33);
        private Color drawColor = Color.FromArgb(50, 0x99, 0xcc);
        private bool fontTextRenderingHint;
        private int imageHeight = 30;
        private int padding = 1;
        private int validataCodeLength = 4;
        private int validataCodeSize = 0x10;
        private string validateCodeFont = "Arial";

        /// <summary>
        /// 创建验证码抽象方法
        /// </summary>
        /// <param name="validataCode">验证码</param>
        /// <returns>数组</returns>
        public override byte[] CreateImage(out string validataCode)
        {
            Bitmap _bitmap;
            string _formatString = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            GetRandom(_formatString, this.ValidataCodeLength, out validataCode);
            using (MemoryStream stream = new MemoryStream())
            {
                this.ImageBmp(out _bitmap, validataCode);
                _bitmap.Save(stream, ImageFormat.Png);
                _bitmap.Dispose();
                _bitmap = null;
                return stream.GetBuffer();
            }
        }

        private void CreateImageBmp(ref Bitmap bitMap, string validateCode)
        {
            Graphics _graphics = Graphics.FromImage(bitMap);
            if (this.fontTextRenderingHint)
            {
                _graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            }
            else
            {
                _graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            }
            Font _font = new Font(this.validateCodeFont, (float)this.validataCodeSize, FontStyle.Regular);
            Brush _brush = new SolidBrush(this.drawColor);
            int _maxValue = Math.Max((this.ImageHeight - this.validataCodeSize) - 5, 0);
            Random _random = new Random();
            for (int i = 0; i < this.validataCodeLength; i++)
            {
                int[] _numArray = new int[] { ((i * this.validataCodeSize) + _random.Next(1)) + 3, _random.Next(_maxValue) - 4 };
                Point _point = new Point(_numArray[0], _numArray[1]);
                _graphics.DrawString(validateCode[i].ToString(), _font, _brush, (PointF)_point);
            }
            _graphics.Dispose();
        }

        private void DisposeImageBmp(ref Bitmap bitmap)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            Pen pen = new Pen(this.DrawColor, 1f);
            new Random();
            Point[] pointArray = new Point[2];
            Random random = new Random();
            if (this.Chaos)
            {
                pen = new Pen(this.ChaosColor, 1f);
                for (int i = 0; i < (this.validataCodeLength * 2); i++)
                {
                    pointArray[0] = new Point(random.Next(bitmap.Width), random.Next(bitmap.Height));
                    pointArray[1] = new Point(random.Next(bitmap.Width), random.Next(bitmap.Height));
                    graphics.DrawLine(pen, pointArray[0], pointArray[1]);
                }
            }
            graphics.Dispose();
        }

        private static void GetRandom(string formatString, int len, out string codeString)
        {
            codeString = string.Empty;
            string[] strArray = formatString.Split(new char[] { ',' });
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = random.Next(0x186a0) % strArray.Length;
                codeString = codeString + strArray[index].ToString();
            }
        }

        private void ImageBmp(out Bitmap bitMap, string validataCode)
        {
            int width = (int)(((this.validataCodeLength * this.validataCodeSize) * 1.3) + 4.0);
            bitMap = new Bitmap(width, this.ImageHeight);
            this.DisposeImageBmp(ref bitMap);
            this.CreateImageBmp(ref bitMap, validataCode);
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }

        /// <summary>
        /// 是否干扰线
        /// </summary>
        public bool Chaos
        {
            get
            {
                return this.chaos;
            }
            set
            {
                this.chaos = value;
            }
        }

        /// <summary>
        /// 干扰色
        /// </summary>
        public Color ChaosColor
        {
            get
            {
                return this.chaosColor;
            }
            set
            {
                this.chaosColor = value;
            }
        }

        /// <summary>
        /// 绘画色
        /// </summary>
        public Color DrawColor
        {
            get
            {
                return this.drawColor;
            }
            set
            {
                this.drawColor = value;
            }
        }

        private bool FontTextRenderingHint
        {
            get
            {
                return this.fontTextRenderingHint;
            }
            set
            {
                this.fontTextRenderingHint = value;
            }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int ImageHeight
        {
            get
            {
                return this.imageHeight;
            }
            set
            {
                this.imageHeight = value;
            }
        }

        /// <summary>
        /// 验证码类型名称
        /// </summary>
        public override string Name
        {
            get
            {
                return "线条干扰(蓝色)";
            }
        }

        public int Padding
        {
            get
            {
                return this.padding;
            }
            set
            {
                this.padding = value;
            }
        }

        public int ValidataCodeLength
        {
            get
            {
                return this.validataCodeLength;
            }
            set
            {
                this.validataCodeLength = value;
            }
        }

        public int ValidataCodeSize
        {
            get
            {
                return this.validataCodeSize;
            }
            set
            {
                this.validataCodeSize = value;
            }
        }

        public string ValidateCodeFont
        {
            get
            {
                return this.validateCodeFont;
            }
            set
            {
                this.validateCodeFont = value;
            }
        }
    }
}