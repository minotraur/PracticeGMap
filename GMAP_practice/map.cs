using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace GMAP_practice
{
    public partial class map : UserControl
    {

        public class CPoint
        {
            public double x { get; set; }
            public double y { get; set; }
            public string place { get; set; }

            public string Image;

            public CPoint() { }
            public CPoint(double _x, double _y, string _place)
            {
                x = _x;
                y = _y;
                place = _place;
            }
        }

        public map()
        {
            InitializeComponent();
        }

        // Список точек для учереждений
        List<CPoint> ListWithPoinsOfMarkets = new List<CPoint>();
        List<CPoint> ListWithPoinsOfFairs = new List<CPoint>();
        List<CPoint> ListWithPoinsOfWeekendFairs = new List<CPoint>();

        // Список слоёв к каждому месту
        GMapOverlay ListOfMarkets = new GMapOverlay("Рынок");
        GMapOverlay ListOfFairs = new GMapOverlay("Ярмарка");
        GMapOverlay ListOfWeekendFairs = new GMapOverlay("Ярмарка выходного дня");

        private void gMap_Load(object sender, EventArgs e)
        {
            // Настройки для компонента GMap
            gMap.Bearing = 0;
            // Перетаскивание левой кнопки мыши
            gMap.CanDragMap = true;
            // Перетаскивание карты левой кнопкой мыши
            gMap.DragButton = MouseButtons.Left;

            gMap.GrayScaleMode = true;

            // Все маркеры будут показаны
            gMap.MarkersEnabled = true;
            // Максимальное приближение
            gMap.MaxZoom = 18;
            // Минимальное приближение
            gMap.MinZoom = 3;
            // Курсор мыши в центр карты
            gMap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;

            // Отключение нигативного режима
            gMap.NegativeMode = false;
            // Разрешение полигонов
            gMap.PolygonsEnabled = true;
            // Разрешение маршрутов
            gMap.RoutesEnabled = true;
            // Скрытие внешней сетки карты
            gMap.ShowTileGridLines = false;
            // При загрузке 10-кратное увеличение
            gMap.Zoom = 11;
            // Убрать красный крестик по центру
            gMap.ShowCenter = false;

            // Чья карта используется
            gMap.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.Position = new PointLatLng(55.753744, 37.620473);

            // Для запросов
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfMarkets);

            // Создали документ
            XmlDocument xml = new XmlDocument();
            // Открыли его по пути
            xml.Load(@"Data\markets.xml");
            XmlElement element = xml.DocumentElement;

            // Элементы ХМЛ-документа
            foreach (XmlElement xnode in element)
            {
                CPoint cPoint = new CPoint();
                // Если атрибут = place
                XmlNode attr = xnode.Attributes.GetNamedItem("array");

                // У каждого узла смотрим его поля
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "X")
                        cPoint.x = Convert.ToDouble(childnode.InnerText);
                    if (childnode.Name == "Y")
                        cPoint.y = Convert.ToDouble(childnode.InnerText);
                    if (childnode.Name == "Name")
                        cPoint.place = childnode.InnerText.ToString();
                }
                ListWithPoinsOfMarkets.Add(cPoint);
            }

            for (int i = 0; i < ListWithPoinsOfMarkets.Count; i++)
            {
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng
                    (ListWithPoinsOfMarkets[i].x, ListWithPoinsOfMarkets[i].y), GMarkerGoogleType.red_dot);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = ListWithPoinsOfMarkets[i].place;
                ListOfMarkets.Markers.Add(marker);
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfFairs);

            // Создали документ
            XmlDocument xml = new XmlDocument();
            // Открыли его по пути
            xml.Load(@"Data\fleas.xml");
            XmlElement element = xml.DocumentElement;

            // Элементы ХМЛ-документа
            foreach (XmlElement xnode in element)
            {
                CPoint cPoint = new CPoint();
                // Если атрибут = place
                XmlNode attr = xnode.Attributes.GetNamedItem("array");

                // У каждого узла смотрим его поля
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "X")
                        cPoint.x = Convert.ToDouble(childnode.InnerText);
                    if (childnode.Name == "Y")
                        cPoint.y = Convert.ToDouble(childnode.InnerText);
                    if (childnode.Name == "Goods")
                        cPoint.place = childnode.InnerText.ToString();
                }
                ListWithPoinsOfFairs.Add(cPoint);
            }

            for (int i = 0; i < ListWithPoinsOfFairs.Count; i++)
            {
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng
                    (ListWithPoinsOfFairs[i].x, ListWithPoinsOfFairs[i].y), GMarkerGoogleType.yellow_dot);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = ListWithPoinsOfFairs[i].place;
                ListOfFairs.Markers.Add(marker);
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfWeekendFairs);

            // Создали документ
            XmlDocument xml = new XmlDocument();
            // Открыли его по пути
            xml.Load(@"Data\weekendfleas.xml");
            XmlElement element = xml.DocumentElement;

            // Элементы ХМЛ-документа
            foreach (XmlElement xnode in element)
            {
                CPoint cPoint = new CPoint();
                // Если атрибут = place
                XmlNode attr = xnode.Attributes.GetNamedItem("array");

                // У каждого узла смотрим его поля
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "X")
                        cPoint.x = Convert.ToDouble(childnode.InnerText);
                    if (childnode.Name == "Y")
                        cPoint.y = Convert.ToDouble(childnode.InnerText);
                    if (childnode.Name == "GoodsCategory")
                        cPoint.place = childnode.InnerText.ToString();
                }
                ListWithPoinsOfWeekendFairs.Add(cPoint);
            }

            for (int i = 0; i < ListWithPoinsOfWeekendFairs.Count; i++)
            {
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng
                    (ListWithPoinsOfWeekendFairs[i].x, ListWithPoinsOfWeekendFairs[i].y), GMarkerGoogleType.orange_dot);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = ListWithPoinsOfWeekendFairs[i].place;
                ListOfWeekendFairs.Markers.Add(marker);
            }
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            ListOfMarkets.Clear();
            ListWithPoinsOfMarkets.Clear();
            ListOfFairs.Clear();
            ListWithPoinsOfFairs.Clear();
            ListOfWeekendFairs.Clear();
            ListWithPoinsOfWeekendFairs.Clear();
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = GMapProviders.GoogleMap;
            gMap.Zoom = 11;
            gMap.Position = new PointLatLng(55.753744, 37.620473);
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = GMapProviders.GoogleSatelliteMap;
            gMap.Zoom = 11;
            gMap.Position = new PointLatLng(55.753744, 37.620473);
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = GMapProviders.GoogleHybridMap;
            gMap.Zoom = 11;
            gMap.Position = new PointLatLng(55.753744, 37.620473);
        }
    }
}
