using System;

class Program {
    static void Main(string[] args) {
      String username;
      String password;

      String[] feeds = new string [] {
        "Pagi yang Cerah",
        "belajar di alkedemi",
        "bandung dingin",
        "malang kota pelajar",
        "kota kembang"
      };
      String Ubenar = "fajar";
      String Pbenar = "fajar";

      Console.WriteLine("Masukkan username : ");
      username = Console.ReadLine();

      Console.WriteLine("Masukkan password : ");
      password = Console.ReadLine();

      if((username==Ubenar) && (password==Pbenar)){
        Console.WriteLine("--------------------------");
        Console.WriteLine("selamat datang di Facebook");
        Console.WriteLine("----------feeds-----------");
        for (int i = 0; i < 5; i++)
            {
              Console.WriteLine(feeds[i]);
            }
      }else{
        Console.WriteLine("--------------------------");
        Console.WriteLine("username dan password anda salah");
      }

      



    }
}