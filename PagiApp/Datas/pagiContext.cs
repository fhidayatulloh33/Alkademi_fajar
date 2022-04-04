using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PagiApp.Datas.Entities;

namespace PagiApp.Datas
{
    public partial class pagiContext : DbContext
    {
        public pagiContext()
        {
        }

        public pagiContext(DbContextOptions<pagiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Alamat> Alamats { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Detailorder> Detailorders { get; set; } = null!;
        public virtual DbSet<KategoriProduct> KategoriProducts { get; set; } = null!;
        public virtual DbSet<Keranjang> Keranjangs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Pembayaran> Pembayarans { get; set; } = null!;
        public virtual DbSet<Pengiriman> Pengirimen { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductKategori> ProductKategoris { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=pagi", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PRIMARY");

                entity.ToTable("admin");

                entity.Property(e => e.IdAdmin)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_admin");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(13)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Alamat>(entity =>
            {
                entity.HasKey(e => e.IdAlamat)
                    .HasName("PRIMARY");

                entity.ToTable("alamat");

                entity.HasIndex(e => e.IdCustomer, "id_customer");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.Desa)
                    .HasMaxLength(15)
                    .HasColumnName("desa");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(255)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.Kabupaten)
                    .HasMaxLength(50)
                    .HasColumnName("kabupaten");

                entity.Property(e => e.Kecamatan)
                    .HasMaxLength(15)
                    .HasColumnName("kecamatan");

                entity.Property(e => e.KodePos)
                    .HasMaxLength(5)
                    .HasColumnName("kode_pos");

                entity.Property(e => e.Provinsi)
                    .HasMaxLength(15)
                    .HasColumnName("provinsi");

                entity.Property(e => e.RtRw)
                    .HasMaxLength(10)
                    .HasColumnName("rt_rw");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Alamats)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("alamat_ibfk_1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PRIMARY");

                entity.ToTable("customer");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Foto)
                    .HasMaxLength(255)
                    .HasColumnName("foto");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(13)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Detailorder>(entity =>
            {
                entity.HasKey(e => e.IdDetaiOrder)
                    .HasName("PRIMARY");

                entity.ToTable("detailorder");

                entity.HasIndex(e => e.IdOrder, "id_order");

                entity.HasIndex(e => e.IdProduct, "id_product");

                entity.Property(e => e.IdDetaiOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_detai_order");

                entity.Property(e => e.Harga)
                    .HasPrecision(20)
                    .HasColumnName("harga");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product");

                entity.Property(e => e.JmlBarang)
                    .HasColumnType("int(20)")
                    .HasColumnName("jml_barang");

                entity.Property(e => e.SubTotal)
                    .HasPrecision(20)
                    .HasColumnName("sub_total");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Detailorders)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detailorder_ibfk_1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Detailorders)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detailorder_ibfk_2");
            });

            modelBuilder.Entity<KategoriProduct>(entity =>
            {
                entity.HasKey(e => e.IdKategori)
                    .HasName("PRIMARY");

                entity.ToTable("kategori_product");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(10)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Icon)
                    .HasMaxLength(225)
                    .HasColumnName("icon");

                entity.Property(e => e.Nama)
                    .HasMaxLength(10)
                    .HasColumnName("nama");
            });

            modelBuilder.Entity<Keranjang>(entity =>
            {
                entity.HasKey(e => e.IdKeranjang)
                    .HasName("PRIMARY");

                entity.ToTable("keranjang");

                entity.HasIndex(e => e.IdCustomer, "id_customer");

                entity.HasIndex(e => e.IdProduct, "id_product");

                entity.Property(e => e.IdKeranjang)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_keranjang");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product");

                entity.Property(e => e.JmlBarang)
                    .HasColumnType("int(3)")
                    .HasColumnName("jml_barang");

                entity.Property(e => e.Subtotol)
                    .HasColumnType("int(15)")
                    .HasColumnName("subtotol");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("keranjang_ibfk_1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("keranjang_ibfk_2");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.IdCustomer, "id_customer");

                entity.HasIndex(e => e.IdKeranjang, "id_keranjang");

                entity.HasIndex(e => e.IdStatus, "id_status");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdKeranjang)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_keranjang");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_status");

                entity.Property(e => e.JmlBayar)
                    .HasColumnType("int(15)")
                    .HasColumnName("jml_bayar");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .HasColumnName("note");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.Property(e => e.TglTransaksi).HasColumnName("tgl_transaksi");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_2");

                entity.HasOne(d => d.IdKeranjangNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdKeranjang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_1");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_3");
            });

            modelBuilder.Entity<Pembayaran>(entity =>
            {
                entity.HasKey(e => e.IdPembayaran)
                    .HasName("PRIMARY");

                entity.ToTable("pembayaran");

                entity.HasIndex(e => e.IdCustomer, "id_customer");

                entity.HasIndex(e => e.IdOrder, "id_order");

                entity.Property(e => e.IdPembayaran)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pembayaran");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.JumlahBayar)
                    .HasColumnType("int(15)")
                    .HasColumnName("jumlah_bayar");

                entity.Property(e => e.MetodeBayar)
                    .HasMaxLength(10)
                    .HasColumnName("metode_bayar");

                entity.Property(e => e.Pajak)
                    .HasColumnType("int(10)")
                    .HasColumnName("pajak");

                entity.Property(e => e.TglBayar).HasColumnName("tgl_bayar");

                entity.Property(e => e.Tujuan)
                    .HasMaxLength(10)
                    .HasColumnName("tujuan");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pembayaran_ibfk_2");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pembayaran_ibfk_1");
            });

            modelBuilder.Entity<Pengiriman>(entity =>
            {
                entity.HasKey(e => e.IdPengiriman)
                    .HasName("PRIMARY");

                entity.ToTable("pengiriman");

                entity.HasIndex(e => e.IdAlamat, "id_alamat");

                entity.HasIndex(e => e.IdOrder, "id_order");

                entity.Property(e => e.IdPengiriman)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pengiriman");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.Kurir)
                    .HasMaxLength(10)
                    .HasColumnName("kurir");

                entity.Property(e => e.Ongkir)
                    .HasColumnType("int(10)")
                    .HasColumnName("ongkir");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengiriman_ibfk_1");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pengiriman_ibfk_2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(100)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Gambar)
                    .HasMaxLength(255)
                    .HasColumnName("gambar");

                entity.Property(e => e.Harga)
                    .HasPrecision(10)
                    .HasColumnName("harga");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(4)")
                    .HasColumnName("stock");
            });

            modelBuilder.Entity<ProductKategori>(entity =>
            {
                entity.HasKey(e => e.IdProductKategori)
                    .HasName("PRIMARY");

                entity.ToTable("product_kategori");

                entity.HasIndex(e => e.IdKategori, "id_kategori");

                entity.HasIndex(e => e.IdProduct, "id_product");

                entity.Property(e => e.IdProductKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product_kategori");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product");

                entity.HasOne(d => d.IdKategoriNavigation)
                    .WithMany(p => p.ProductKategoris)
                    .HasForeignKey(d => d.IdKategori)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_kategori_ibfk_1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductKategoris)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_kategori_ibfk_2");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_status");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(50)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .HasColumnName("nama");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
