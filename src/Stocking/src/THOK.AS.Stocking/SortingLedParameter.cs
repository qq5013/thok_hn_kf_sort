using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.AS.Stocking
{
    public class SortingLedParameter : BaseObject
    {
        private int port;

        [CategoryAttribute("����ϵͳͨ�Ų���"), DescriptionAttribute("����ϵͳ�����˿�"), Chinese("�����˿�")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string ip;

        [CategoryAttribute("����ϵͳͨ�Ų���"), DescriptionAttribute("����ϵͳIP��ַ"), Chinese("IP��ַ")]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private string maxLEDFormTitle;

        [CategoryAttribute("������ʾ����"), DescriptionAttribute("�������"), Chinese("����")]
        public string MaxLEDFormTitle
        {
            get { return maxLEDFormTitle; }
            set { maxLEDFormTitle = value; }
        }

        private string maxLEDFormTop;

        [CategoryAttribute("������ʾ����"), DescriptionAttribute("Top"), Chinese("Top")]
        public string MaxLEDFormTop
        {
            get { return maxLEDFormTop; }
            set { maxLEDFormTop = value; }
        }

        private string maxLEDFormLeft;

        [CategoryAttribute("������ʾ����"), DescriptionAttribute("Left"), Chinese("Left")]
        public string MaxLEDFormLeft
        {
            get { return maxLEDFormLeft; }
            set { maxLEDFormLeft = value; }
        }

        private string maxLEDFormWidth;

        [CategoryAttribute("������ʾ����"), DescriptionAttribute("Width"), Chinese("Width")]
        public string MaxLEDFormWidth
        {
            get { return maxLEDFormWidth; }
            set { maxLEDFormWidth = value; }
        }

        private string maxLEDFormHeight;

        [CategoryAttribute("������ʾ����"), DescriptionAttribute("Height"), Chinese("Height")]
        public string MaxLEDFormHeight
        {
            get { return maxLEDFormHeight; }
            set { maxLEDFormHeight = value; }
        }


    }
}
