using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;

namespace ADONET_PROJE
{
     class Program
    {
       public static SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True"); 
        static void Main(string[] args)
        {
            KayitGetirAdaptor();
            Console.WriteLine("LÜTFEN YAPMAK İSTEDİĞİNİZ İŞLEMİ SEÇİN");
            Console.WriteLine("WHERE (W)\t INSERT (I)\t UPDATE (U)\t DELETE (D)");
            string islem = Console.ReadLine();
            if (islem.ToUpper()=="W")
            {
                Console.WriteLine("LÜTFEN ID'Yİ SEÇİNİZ");
                KayitGetirReader(Console.ReadLine());
            }
            else if (islem.ToUpper()=="I")
            {
                KayitEkle();
            }
            else if (islem.ToUpper()=="U")
            {
                Console.WriteLine("LÜTFEN ID'Yİ GİRİNİZ");
                string id = Console.ReadLine();
                KayitGuncelle(id);
                KayitGetirReader(id);
            }
            else if (islem.ToUpper()=="D")
            {
                Console.WriteLine("LÜTFEN ID'Yİ GİRİNİZ");
                KayitSil(Console.ReadLine());
            }
            Console.ReadKey();
            

        }
        public static void KayitGetirAdaptor()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Personeller",cnn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            

            

            Console.WriteLine("ID\tSoyadi Adi \tUnvan}\tUnvanEki");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine(ds.Tables[0].Rows[i][0] + "\t" + ds.Tables[0].Rows[i][1] + "\t" + ds.Tables[0].Rows[i][2] + "\t" + ds.Tables[0].Rows[i][3] + "\t" + ds.Tables[0].Rows[i][4]);
            }

        }
        public static void KayitGetirReader(string id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Personeller WHERE Id=@Id"+id+ "" ,cnn);
            cmd.Parameters.AddWithValue("@Id", id);
            cnn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Console.WriteLine("ID\tSoyadi Adi \tUnvan}\tUnvanEki");

            while (dr.Read())
            {
                Console.WriteLine(dr[0] +"\t"+ dr[1] +"\t"+ dr[2] +"\t"+ dr[3] +"\t"+ dr[4].ToString());
            }
            dr.Close();
            cnn.Close();
        }
        public static void KayitEkle()
        {
            string adi, soyadi, adress, telefon;
            Console.WriteLine("LÜTFEN AD GİRİN");
            adi= Console.ReadLine();
            Console.WriteLine("LÜTFEN SOYADİ GİRİN");
            soyadi= Console.ReadLine();
            Console.WriteLine("LÜTFEN UNVAN GİRİN");
            adress= Console.ReadLine();
            Console.WriteLine("LÜTFEN UNVANEKI GİRİN");
            telefon= Console.ReadLine();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO dbo.Personeller (Soyadi,Adi,Unvan,Unvaneki) VALUES ('"+adi+"'"+soyadi+"'"+adress+"'"+telefon+")";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();

            int kayit = cmd.ExecuteNonQuery();
            cnn.Close();
            Console.WriteLine(kayit.ToString()+"ADET VERİ EKLENDİ");
        }
        public static void KayitGuncelle(string id)
        {
            string adi, adres;
            Console.WriteLine("ADİNİZİ GİRİNİZ : ");
            adi = Console.ReadLine();
            Console.WriteLine("UNVAN GİRİNİZ : ");
            adres = Console.ReadLine();
            
            SqlCommand cmd =new SqlCommand("Update dbo.Personeller Set Adi ="+adi+"'UNVAN ='"+adres+"'WHERE ID='"+id+"",cnn);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            Console.WriteLine("GÜNCELLEME İŞLEMİ BAŞARILI ŞEKİLDE GERÇEKLEŞTİ");
        }
        public static void KayitSil(string id)
        {
            SqlCommand cmd = new SqlCommand("SP_delete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id",id);

            cnn.Open();
            cmd.ExecuteReader();
            cnn.Close();

            Console.WriteLine("VERİ BAŞARILI BIR SEKILDE SİLİNDİ");
        }
    }
}
