namespace THOK.AS.Stocking.Util
{
    public class Character
    {
        // Fields
        private int _c1;
        private int _c2;
        private int _Height;
        private int _Width;
        private string _ID;
        private int _Itailc;
        private int _Orientation;
        private int _pLeft;
        private string _printData;
        private int _pTop;
        private string _SysFontName;
        private int _XZoom;
        private int _YZoom;

        // Methods
        public Character(int pLeft, int pTop, string Data, string ID, int Height, int Width, string SysFontName, int Itailc, int Orientation, int XZoom, int YZoom)
        {
            this._Height = Height;//����߶�
            this._Width = Width;//������
            this._ID = ID;
            this._Itailc = Itailc;//б��
            this._Orientation = Orientation;//��ӡ���ݵ���ת�Ƕ�
            this._pLeft = pLeft;//�����ӡֽ��ߵĳ���
            this._printData = Data;//��ʾ����
            this._pTop = pTop;//�����ӡ���˵ĳ���
            this._SysFontName = SysFontName;//��������
            this._XZoom = XZoom;//X������ų̶�
            this._YZoom = YZoom;//Y������ų̶�
        }

        // Properties
        public int c1
        {
            get
            {
                return 0;
            }
        }

        public int c2
        {
            get
            {
                return 0;
            }
        }

        public int Height
        {
            get
            {
                return this._Height;
            }
        }

        public int Width
        {
            get
            {
                return this._Width;
            }
        }

        public string ID
        {
            get
            {
                return this._ID;
            }
        }

        public int Itailc
        {
            get
            {
                return this._Itailc;
            }
        }

        public int Orientation
        {
            get
            {
                return this._Orientation;
            }
        }

        public int pLeft
        {
            get
            {
                return this._pLeft;
            }
        }

        public string printData
        {
            get
            {
                return this._printData;
            }
        }

        public int pTop
        {
            get
            {
                return this._pTop;
            }
        }

        public string SysFontName
        {
            get
            {
                return this._SysFontName;
            }
        }

        public int XZoom
        {
            get
            {
                return this._XZoom;
            }
        }

        public int YZoom
        {
            get
            {
                return this._YZoom;
            }
        }
    }
}
