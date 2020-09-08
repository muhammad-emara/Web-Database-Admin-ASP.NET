using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace DbMgmAdmin.classes
{
    public class clsPhotoManager
    {

        // Photo-Related Methods

        public static Stream GetPhoto(int photoid, PhotoSize size)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("GetPhoto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@PhotoID", photoid));
                    command.Parameters.Add(new SqlParameter("@Size", (int)size));
                    bool filter = !(HttpContext.Current.User.IsInRole("Friends") || HttpContext.Current.User.IsInRole("Administrators"));
                    command.Parameters.Add(new SqlParameter("@IsPublic", filter));
                    connection.Open();
                    object result = command.ExecuteScalar();
                    try
                    {
                        return new MemoryStream((byte[])result);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public static Stream GetPhoto(PhotoSize size)
        {
            string path = HttpContext.Current.Server.MapPath("~/Images/");
            switch (size)
            {
                case PhotoSize.Small:
                    path += "placeholder-100.jpg";
                    break;
                case PhotoSize.Medium:
                    path += "placeholder-200.jpg";
                    break;
                case PhotoSize.Large:
                    path += "placeholder-600.jpg";
                    break;
                default:
                    path += "placeholder-600.jpg";
                    break;
            }
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public static Stream GetFirstPhoto(int albumid, PhotoSize size)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("GetFirstPhoto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AlbumID", albumid));
                    command.Parameters.Add(new SqlParameter("@Size", (int)size));
                    bool filter = false;
                    command.Parameters.Add(new SqlParameter("@IsPublic", filter));
                    connection.Open();
                    object result = command.ExecuteScalar();
                    try
                    {
                        return new MemoryStream((byte[])result);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public static ArrayList GetPhotos(int AlbumID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("GetPhotos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AlbumID", AlbumID));
                    bool filter = false;
                    command.Parameters.Add(new SqlParameter("@IsPublic", filter));
                    connection.Open();
                    ArrayList Photolist = new ArrayList();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Photo temp = new Photo(
                                (int)reader["PhotoID"],
                                (int)reader["AlbumID"],
                                (string)reader["Caption"]);
                            Photolist.Add(temp);
                        }
                    }
                    return Photolist;
                }
            }


        }

        public static ArrayList GetPhotos()
        {
            return GetPhotos(GetRandomAlbumID());
        }

        public static void AddPhoto(int AlbumID, string Caption, byte[] BytesOriginal)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("AddPhoto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AlbumID", AlbumID));
                    command.Parameters.Add(new SqlParameter("@Caption", Caption));
                    command.Parameters.Add(new SqlParameter("@BytesOriginal", BytesOriginal));
                    command.Parameters.Add(new SqlParameter("@BytesFull", ResizeImageFile(BytesOriginal, 600)));
                    command.Parameters.Add(new SqlParameter("@BytesPoster", ResizeImageFile(BytesOriginal, 198)));
                    command.Parameters.Add(new SqlParameter("@BytesThumb", ResizeImageFile(BytesOriginal, 100)));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RemovePhoto(int PhotoID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("RemovePhoto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@PhotoID", PhotoID));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void EditPhoto(string Caption, int PhotoID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("EditPhoto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Caption", Caption));
                    command.Parameters.Add(new SqlParameter("@PhotoID", PhotoID));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Album-Related Methods

        public static ArrayList GetAlbums()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("GetAlbums", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    bool filter = false;
                    command.Parameters.Add(new SqlParameter("@IsPublic", filter));
                    connection.Open();
                    ArrayList Albumlist = new ArrayList();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Album temp = new Album(
                                (int)reader["AlbumID"],
                                (int)reader["NumberOfPhotos"],
                                (string)reader["Caption"],
                                (bool)reader["IsPublic"]);
                            Albumlist.Add(temp);
                        }
                    }
                    return Albumlist;
                }
            }
        }

        public static void AddAlbum(string Caption, bool IsPublic)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("AddAlbum", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Caption", Caption));
                    command.Parameters.Add(new SqlParameter("@IsPublic", IsPublic));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RemoveAlbum(int AlbumID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("RemoveAlbum", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AlbumID", AlbumID));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void EditAlbum(string Caption, bool IsPublic, int AlbumID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("EditAlbum", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Caption", Caption));
                    command.Parameters.Add(new SqlParameter("@IsPublic", IsPublic));
                    command.Parameters.Add(new SqlParameter("@AlbumID", AlbumID));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static int GetRandomAlbumID()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand("GetNonEmptyAlbums", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    ArrayList Albumlist = new ArrayList();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Album temp = new Album((int)reader["AlbumID"], 0, "", false);
                            Albumlist.Add(temp);
                        }
                    }

                    try
                    {
                        Random r = new Random();
                        Album myA = (Album)Albumlist[r.Next(Albumlist.Count)];
                        return myA.AlbumID;
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
        }

        // Helper Functions

        private static byte[] ResizeImageFile(byte[] imageFile, int targetSize)
        {
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return m.GetBuffer();
                    }
                }
            }
        }

        private static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }

        public static ICollection ListUploadDirectory()
        {
            DirectoryInfo d = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Upload"));
            return d.GetFileSystemInfos("*.jpg");
        }

    }
    public class Album
    {

        private int _id;
        private int _count;
        private string _caption;
        private bool _ispublic;

        public int AlbumID { get { return _id; } }
        public int Count { get { return _count; } }
        public string Caption { get { return _caption; } }
        public bool IsPublic { get { return _ispublic; } }

        public Album(int id, int count, string caption, bool ispublic)
        {
            _id = id;
            _count = count;
            _caption = caption;
            _ispublic = ispublic;
        }

    }
    public class Photo
    {

        private int _id;
        private int _albumid;
        private string _caption;

        public int PhotoID { get { return _id; } }
        public int AlbumID { get { return _albumid; } }
        public string Caption { get { return _caption; } }

        public Photo(int id, int albumid, string caption)
        {
            _id = id;
            _albumid = albumid;
            _caption = caption;
        }

    }

    public enum PhotoSize
    {
        Small = 1,      // 100px
        Medium = 2,     // 198px
        Large = 3,      // 600px
        Original = 4    // Original Size
    }
}