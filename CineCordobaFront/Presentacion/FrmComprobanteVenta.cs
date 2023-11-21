using CineCordobaBack.Entidades;
using CineCordobaFront.Cliente;
using CineCordobaFront.Servicios.Implementacion;
using CineCordobaFront.Servicios.Interfaz;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CineCordobaFront.Presentacion
{
    public partial class FrmComprobanteVenta : Form
    {

        Comprobantes nuevo = null;
        int proxComprobante;


        public FrmComprobanteVenta()
        {
            InitializeComponent();

            nuevo = new Comprobantes();


        }


        private void LimpiarCampos()
        {
            txtDocumento.Text = string.Empty;
            txtCodigoVendedor.Text = string.Empty;
            txtSubtotal.Text = string.Empty;
            txtTotal.Text = string.Empty;
            cboVendedor.SelectedIndex = -1;
            cboCliente.SelectedIndex = -1;
            cboFormasPago.SelectedIndex = -1;
            cbofuncion.SelectedIndex = -1;
            cboPeli.SelectedIndex = -1;
            cboPromo.SelectedIndex = -1;
            cboSucursal.SelectedIndex = -1;
            cboTipoSalas.SelectedIndex = -1;
            dgvComprobante.Rows.Clear();
            dtpFechaVenta.Value = DateTime.Now;
        }

        private async void FrmComprobanteVenta_Load(object sender, EventArgs e)
        {
            dtpFechaVenta.Value = DateTime.Now;
            dtpFechaVenta.Enabled = false;
            txtCodigoVendedor.Enabled = false;
            gboxPeli.Enabled = false;
            gboxPromo.Enabled = false;
            btnConfirmar.Enabled = false;

            await ProximoComprobante();
            LimpiarCampos();
            await CargarCombos();


        }
        private async Task RealizarOperacionAsincrona()
        {
            await Task.Delay(3000);
        }

        private async Task ProximoComprobante()
        {
            string url = "https://localhost:7051/proxComprobante";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            proxComprobante = JsonConvert.DeserializeObject<int>(dataJson);

            if (proxComprobante > 0)
            {

                txtNumComprobante.Text = "Comprobante N° : " + proxComprobante.ToString();
            }
            else
            {
                MessageBox.Show("Error en obtener el proximo N° Comprobante.", "ERROR");
            }


        }

        private async Task<int> ProximoDetalleComprobante()
        {
            string url = "https://localhost:7051/proxComprobante";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            int proxDetalle = JsonConvert.DeserializeObject<int>(dataJson);
            return proxDetalle;
        }


        private async Task CargarCombos()
        {

            await CargarFormasPagoAsync();
            await CargarClientesAsync();
            await CargarPeliculasAsync();
            await CargarPromocionesAsync();
            await CargarSucursalesAsync();
            await CargarVendedoresAsync();
        }

        private async Task CargarVendedoresAsync()
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/vendedores";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Vendedores> lVendedores = JsonConvert.DeserializeObject<List<Vendedores>>(dataJson);

            cboVendedor.DataSource = lVendedores;
            cboVendedor.ValueMember = "VendedorId";
            cboVendedor.DisplayMember = "NombreCompleto";
        }

        private async Task CargarSucursalesAsync()
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/sucursales";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Sucursales> lSucursales = JsonConvert.DeserializeObject<List<Sucursales>>(dataJson);

            cboSucursal.DataSource = lSucursales;
            cboSucursal.ValueMember = "SucursalId";
            cboSucursal.DisplayMember = "NombreSucursal";
        }


        private async Task CargarPromocionesAsync()
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/promos";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Promociones> lPromos = JsonConvert.DeserializeObject<List<Promociones>>(dataJson);

            cboPromo.DataSource = lPromos;
            cboPromo.ValueMember = "PromoId";
            cboPromo.DisplayMember = "NombrePromo";
        }

        private async void cboPeli_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPeli = cboPeli.SelectedIndex + 1;

            await CargarFuncionesAsync(idPeli);

        }
        private async void cbofuncion_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RealizarOperacionAsincrona();
            int idFuncion = Convert.ToInt32(cbofuncion.SelectedIndex + 1);
            await CargarTipoSalasAsync(idFuncion);
        }

        private void cboTipoSalas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idTipoSala = Convert.ToInt32(cboTipoSalas.SelectedIndex + 1);

        }

        private void cboVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoVendedor.Text = cboVendedor.SelectedValue.ToString();
        }



        private async Task CargarPeliculasAsync()
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/peliculas";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Peliculas> lPeliculas = JsonConvert.DeserializeObject<List<Peliculas>>(dataJson);

            cboPeli.DataSource = lPeliculas;
            cboPeli.ValueMember = "PeliculaId";
            cboPeli.DisplayMember = "NombrePelicula";
        }

        private async Task CargarFuncionesAsync(int idPeli)
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/funciones" + "?idPeli=" + idPeli;
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Funciones> lFunciones = JsonConvert.DeserializeObject<List<Funciones>>(dataJson);

            cbofuncion.DataSource = lFunciones;
            cbofuncion.ValueMember = "FuncionId";
            cbofuncion.DisplayMember = "FuncionCompleta";
        }

        private async Task CargarClientesAsync()
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/clientes";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Clientes> lClientes = JsonConvert.DeserializeObject<List<Clientes>>(dataJson);

            cboCliente.DataSource = lClientes;
            cboCliente.ValueMember = "ClienteId";
            cboCliente.DisplayMember = "NombreCompleto";
        }

        private async Task CargarFormasPagoAsync()
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/formasDePago";
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<FormasPago> lformasPago = JsonConvert.DeserializeObject<List<FormasPago>>(dataJson);

            cboFormasPago.DataSource = lformasPago;
            cboFormasPago.ValueMember = "FormasPagoId";
            cboFormasPago.DisplayMember = "FormasDePago";


        }


        private async Task CargarTipoSalasAsync(int idFuncion)
        {
            await RealizarOperacionAsincrona();
            string url = "https://localhost:7051/tipoSalas" + "?idFuncion=" + idFuncion;
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<TipoSalas> lTipoSalas = JsonConvert.DeserializeObject<List<TipoSalas>>(dataJson);

            cboTipoSalas.DataSource = lTipoSalas;
            cboTipoSalas.ValueMember = "TipoSalasId";
            cboTipoSalas.DisplayMember = "tipo";
        }


        bool existe;    //VARIABLE GLOBAL
        //List<Clientes> lclientes;

        private async Task ExisteDocumento(int doc)
        {
            string url = "https://localhost:7051/existenciaDocumento" + "?numDoc=" + doc;
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            existe = JsonConvert.DeserializeObject<bool>(dataJson);


        }

        private async Task ClienteEncontrado(int doc)
        {
            string url = "https://localhost:7051/clienteFiltrdoPorDoc" + "?numDoc=" + doc;
            var dataJson = await ClienteSingleton.ObtenerIntancia().GetAsync(url);
            List<Clientes> lclientes = JsonConvert.DeserializeObject<List<Clientes>>(dataJson);


            cboCliente.DataSource = lclientes;
            cboCliente.ValueMember = "ClienteId";
            cboCliente.DisplayMember = "NombreCompleto";

        }



        private async void btnBuscarCLiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocumento.Text))   //si el txt documento no esta vacio hace lo siguiente
            {


                int doc = int.Parse(txtDocumento.Text);

                await ExisteDocumento(doc);
                if (existe)
                {
                    await ClienteEncontrado(doc);

                }

            }
        }

        TipoSalas ts;
        private void btnAgregarEntrada_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = true;

            ts = (TipoSalas)cboTipoSalas.SelectedItem;

            Funciones f = (Funciones)cbofuncion.SelectedItem;


            f.Sala.TipoSala = ts;
            Promociones p = (Promociones)cboPromo.SelectedItem;
            string sub;

            if (f.Subtitulo == 0)
            {
                sub = "No";
            }
            else
            {
                sub = "Sí";
            }

            DetalleComprobante detalle = new DetalleComprobante(f, p);
            nuevo.AgregarDetalle(detalle);

            dgvComprobante.Rows.Add(new object[] { f.Pelicula.NombrePelicula, f.FuncionCompleta, f.Sala.SalaId
                                                 , f.Sala.TipoSala.Tipo, sub, f.Sala.TipoSala.Precio.ToString(), p.NombrePromo , "Quitar"});

            CalcularTotales();

        }

        private void CalcularTotales()
        {
            txtSubtotal.Text = nuevo.CalcularTotal().ToString();
            Promociones p = (Promociones)cboPromo.SelectedItem;


            double desc = nuevo.CalcularTotal() * (p.Descuento);
            txtTotal.Text = (nuevo.CalcularTotal() - desc).ToString();

        }


        private void dgvComprobante_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvComprobante.CurrentCell.ColumnIndex == 7) //boton Quitar de la grilla
            {
                nuevo.EliminarDetalle(dgvComprobante.CurrentRow.Index);
                dgvComprobante.Rows.RemoveAt(dgvComprobante.CurrentRow.Index);
                CalcularTotales();
            }
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            CrearComprobante();
        }


        private async void CrearComprobante()
        {

            Clientes cli = (Clientes)cboCliente.SelectedItem;
            Vendedores vendedor = (Vendedores)cboVendedor.SelectedItem;
            Sucursales suc = (Sucursales)cboSucursal.SelectedItem;
            FormasPago fp = (FormasPago)cboFormasPago.SelectedItem;
            //List<DetalleComprobante> dt = new List<DetalleComprobante>();

            foreach (DetalleComprobante i in nuevo.lDetallesComprobantes)
            {
                i.Precio = ts.Precio;
                // i.Precio = Convert.ToDouble(txtTotal.Text);
                i.DetalleComprobanteId = await ProximoDetalleComprobante();



            }

            nuevo.ComprobanteId = proxComprobante;

            nuevo.Cliente = cli;
            nuevo.Vendedor = vendedor;
            nuevo.Sucursal = suc;
            nuevo.FechaComprobante = Convert.ToDateTime(dtpFechaVenta.Value);
            nuevo.FormaPago = fp;
            //nuevo.lDetallesComprobantes = dt;


            if (await CrearComprobanteAsync(nuevo))
            {
                MessageBox.Show("Se registró con éxito el comprobante...", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("NO SE PUDO registrar el comprobante...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task<bool> CrearComprobanteAsync(Comprobantes nuevo)
        {
            string url = "https://localhost:7051/insertComprobante";

            // Llamar al método PostAsync con el objeto Comprobantes
            var dataJson = await ClienteSingleton.ObtenerIntancia().PostAsync(url, nuevo);

            return dataJson.Equals("true");
        }


      

        private void btnElegirPeli_Click(object sender, EventArgs e)
        {
            gboxMaestro.Enabled = false;
            gboxPeli.Enabled = true;
        }

        private void btnElegirPromo_Click(object sender, EventArgs e)
        {
            gboxPromo.Enabled = true;
            gboxPeli.Enabled = false;

        }

        private void btnEditarEntrada_Click(object sender, EventArgs e)
        {
            gboxMaestro.Enabled = true;
            gboxPeli.Enabled = false;
            gboxPromo.Enabled = false;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
           DialogResult result=  MessageBox.Show("Seguro desea SALIR?", "IMPORTANTE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(result == DialogResult.OK)
            {
                this.Dispose();

            }

        }









        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
