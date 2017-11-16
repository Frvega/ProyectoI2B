using ApisBurguer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace ApisBurguer.Controllers
{

    public class LoginController : ApiController
    {
        //private JsonResult JsonRes = new JsonResult();   
        // GET: api/Login
        private BurguesiadbEntities db = new BurguesiadbEntities();
        private String metadata = "Data Source=ROBERTO\\SQLEXPRESS;Initial Catalog=Burguesiadb;User ID=sa;Password=12345;MultipleActiveResultSets=True;Application Name=EntityFramework";

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("api/promociones")]
        public HttpResponseMessage GetPromociones()
        {

            var infantiles = db.promociones.Select(x => new promocionesInfatiles
            {

                idPromocion = x.idPromocion,
                descripcionPromocion = x.descripcionPromocion,
                costoPromocion = x.CostoPromocion,
                categoriaPromocion = x.categoriaPromocion
            }).Where(p=>(p.categoriaPromocion=="Infantil")).ToList();

            var reuniones = db.promociones.Select(x => new promocionesReuniones
            {

                idPromocion = x.idPromocion,
                descripcionPromocion = x.descripcionPromocion,
                costoPromocion = x.CostoPromocion,
                categoriaPromocion = x.categoriaPromocion
            }).Where(p => (p.categoriaPromocion == "Reunion")).ToList();

            var generales = db.promociones.Select(x => new Generales
            {

                idPromocion = x.idPromocion,
                descripcionPromocion = x.descripcionPromocion,
                costoPromocion = x.CostoPromocion,
                categoriaPromocion = x.categoriaPromocion
            }).ToList();

            promocionesGenerales promociones = new promocionesGenerales();

            promociones.Infantiles = infantiles;
            promociones.Reuniones = reuniones;
            promociones.Generales = generales;

            return Request.CreateResponse(HttpStatusCode.OK,promociones );
  
        }
        [HttpPost]
        [Route("api/contratarPromocion")]
        public HttpResponseMessage contratarPromocion( promocionContrada contratado) { 

            using (SqlConnection cn = new SqlConnection(metadata))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("contratarPromocion", cn))
                {
                    //cmd.Parameters.AddWithValue("@idPromocionContratada", Convert.ToInt32(contratado.idPromocionContratada));
                    cmd.Parameters.AddWithValue("@idPromocion", Convert.ToInt32(contratado.idPromocion));
                    cmd.Parameters.AddWithValue("@nombreCliente", Convert.ToString(contratado.nombreCliente));
                    cmd.Parameters.AddWithValue("@apellidoCliente", Convert.ToString(contratado.apellidoCliente));
                    cmd.Parameters.AddWithValue("@direccionCliente", Convert.ToString(contratado.direccionCliente));
                    cmd.Parameters.AddWithValue("@telefonoCliente", Convert.ToString(contratado.telefonoCliente));
                    cmd.Parameters.AddWithValue("@fechaPedido", Convert.ToString(contratado.fechaPedido));
                    cmd.Parameters.AddWithValue("@fechaEntrega", Convert.ToString(contratado.fechaEntrega));


                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        resultadoValidacion result = new resultadoValidacion();
                        while (dr.Read())
                        {
                            result.status = (Int32)dr["Resultado"];
                            result.message = (string)dr["Mensaje"];
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, result);

                    }
                }
            }
            
        }

        [HttpGet]
        [Route("api/listaComidas")]
        public HttpResponseMessage GetLista()
        {
            var lista = new listaComidas();
            using (SqlConnection cn = new SqlConnection(metadata))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listarComidas", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.idComida = (Int32)dr["idComida"];
                            lista.nombreComida = (string)dr["nombreComida"];
                            lista.descripcionComida = (string)dr["descripcionComida"];
                            lista.precioComida = (Int32)dr["precioComida"];
                            lista.imagenComida = (string)dr["imagenComida"];

                        }

                        return Request.CreateResponse(HttpStatusCode.OK,lista);
                    } 
                }
            }
        }

        [HttpGet]
        [Route("api/listaBebidas")]
        public HttpResponseMessage GetListaBebidas()
        {
            var lista = new listaBebidas();
            using (SqlConnection cn = new SqlConnection(metadata))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listarBebidas", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.idBebida = (Int32)dr["idBebida"];
                            lista.nombreBebida = (string)dr["nombreBebida"];
                            lista.descripcionBebida = (string)dr["descripcionBebida"];
                            lista.precioBebida = (Int32)dr["precioBebida"];
                            lista.imagenBebida = (string)dr["imagenBebida"];

                        }

                        return Request.CreateResponse(HttpStatusCode.OK, lista);
                    }
                }
            }
        }

        // POST: api/Login
        [HttpPost]
       [Route("api/login")]
   
        public HttpResponseMessage Post(spLogin_Result loginu)
        {
            var resultado = new resultadoValidacion();

            if (loginu.nombreUsuario != "" && loginu.contrasenaUsuario != "")
            {



                using (SqlConnection cn = new SqlConnection(metadata))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("spLogin", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user", Convert.ToString(loginu.nombreUsuario));
                        cmd.Parameters.AddWithValue("@password", Convert.ToString(loginu.contrasenaUsuario));
                        cmd.Parameters.AddWithValue("@resultadoValidacion", 0).Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@puesto", 0).Direction = ParameterDirection.Output;
                        //var resultado = 0;



                        using (SqlDataReader dr = cmd.ExecuteReader())

                        {
                            //var resultado = new resultadoValidacion();
                            //var user = new spLogin_Result();

                            while (dr.Read())
                            {
                                //user.nombreUsuario = (string)dr["nombreUsuario"];
                                //user.contrasenaUsuario = (string)dr["contrasenaUsuario"];
                                resultado.status = (int)dr["Resultado"];
                                resultado.idPuesto = (int)dr["Puesto"];


                            }
                            //var resultado = Convert.ToInt32( dr.Read());
                            //var resultado = Convert.ToInt32(cmd.Parameters["@resultadoValidacion"].Value);

                            

                            if (resultado.status == 1)
                            {
                                resultado.message = "Bienvenido";

                                return Request.CreateResponse(HttpStatusCode.OK, resultado);
                            }
                            resultado.message = "Credenciales Incorrectas";

                            return Request.CreateResponse(HttpStatusCode.OK, resultado);
                        }

                    }

                }

            }
            else {

                resultado.status = 0;
                resultado.message = "Llene los campos";
                resultado.idPuesto = 0;
                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }

        }
// PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
        //}
    }


    }

        
    
