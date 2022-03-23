using System;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {

      //data feed dalam list
		  List<Feed> feeds=new List<Feed>();
		    feeds.Add(new Feed(){nama="andri",feed="Pagi yang Cerah"});
		    feeds.Add(new Feed(){nama="riki",feed="belajar di alkedemi"});
        feeds.Add(new Feed(){nama="fandi",feed="bandung dingin"});
        feeds.Add(new Feed(){nama="syahrul",feed="kota kembang"});

      // data username dan password benar
      String Ubenar = "fajar";
      String Pbenar = "fajar";

      // input username dan password
      login();
     
      // jika username dan password benar 
      if((User.username==Ubenar) && (User.password==Pbenar)){
        Console.WriteLine("--------------------------");
        Console.WriteLine("selamat datang di Facebook");
        Console.WriteLine("----------feeds-----------");

        // cetak data feed
        // showfeeds();
        foreach(Feed i in feeds){
          Console.WriteLine("Nama: "+i.nama + "\nfeed: " + i.feed+"\n");
        }

        // menambahkan feed
        Console.Write("Masukan feed: ");
        String feedBaru = Console.ReadLine();
        feeds.Insert(0,new Feed{nama = Ubenar, feed = feedBaru});
        foreach(Feed i in feeds){
          Console.WriteLine("Nama: "+i.nama + "\nfeed: " + i.feed+"\n");
        }
      
      // jika username dan password salah
      }else{
        Console.WriteLine("--------------------------");
        Console.WriteLine("username dan password anda salah");
      }
    }
    // method untuk login
    static void login(){
      Console.WriteLine("Masukkan username : ");
      User.username = Console.ReadLine();
      Console.WriteLine("Masukkan password : ");
      User.password = Console.ReadLine();
    }
 }

 // class feed
public class Feed{
	public string nama {get;set;}
	public string feed {get;set;}
}
 
 // class user untuk username dan password
public static class User{
  public static string username;
  public static string password;
}
