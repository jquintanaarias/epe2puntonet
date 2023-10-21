using Microsoft.AspNetCore.Mvc;
using epe2.controllers;

namespace epe2.controllers{}

[Route("Empresa")]
[ApiController]

public class  Empresacontroller : ControllerBase {

public List<Empresa> empresas = new List<Empresa>{

new Empresa {NombreCliente="Jesus", ApellidosCliente="Quintana Arias", RutCliente="283789278-5",EdadCLiente=25,
 NombreEmpresa="Jmeat", RutEmpresa="789978287-0", GiroEmpresa="Transporte", TotalVentas=300f, MontoVentas=3000000f,MontoIVA=19f, MontoUtilidades=5000000f},

 new Empresa {NombreCliente="Jaime", ApellidosCliente="Aguilar Aguilar", RutCliente="12344678-k",EdadCLiente=40,
 NombreEmpresa="ConstructoraJaime", RutEmpresa="800188393-9", GiroEmpresa="Construccion", TotalVentas=700f, MontoVentas=8000000f,MontoIVA=19f, MontoUtilidades=1000000f},

 new Empresa {NombreCliente="Melisa", ApellidosCliente="Bastias Bastias", RutCliente="172393849-5",EdadCLiente=32,
 NombreEmpresa="SiempreBella", RutEmpresa="901992992-7", GiroEmpresa="Articulos de belleza", TotalVentas=300f, MontoVentas=8000000f,MontoIVA=19f, MontoUtilidades=200000},
// Aqui se puede agregar mas clientes de ser necesario

};
 //metodo get para traer a los clientes
        [HttpGet("empresas")]
        public IActionResult GetEmpresas()
        {
            return Ok(empresas);
        }
//metodo GET para llamar empresas
        [HttpGet("Empresas")]
        public IActionResult GetEmpresa()
        {
            // Selecciona empresas diferentes
            var Empresas = empresas.Take(3).ToList();
            return Ok(Empresas);
        }
       

        //Método GET para mostrar los datos de una empresa por su Rut
        [HttpGet("empresas/{rutEmpresa}")]
        public IActionResult GetEmpresa(string RutEmpresa)
        {
            var empresa = empresas.Find(c => c.RutEmpresa == RutEmpresa);
            if (empresa == null)
            {
                return StatusCode(404);
            }
            return Ok(empresa);
        }

        //Método POST para agregar y guardar una empresa nueva
        [HttpPost("empresas")]
        public IActionResult CrearEmpresa([FromBody] Empresa empresa)
        {
            empresas.Add(empresa);
            return CreatedAtAction("GetEmpresa", new { rutEmpresa = empresa.RutEmpresa }, empresa);
        }

        //Método PUT para editar, guardar cambios a una empresa ya creada
        [HttpPut("empresas/{rutEmpresa}")]
        public IActionResult EditarEmpresa(string rutEmpresa, [FromBody] Empresa empresa)
        {
            var empresaExistente = empresas.Find(c => c.RutEmpresa == rutEmpresa);
            if (empresaExistente == null)
            {
                return StatusCode(404);
            }
            return NoContent();
        }

        //Método DELETE como eliminar empresa creada
        [HttpDelete("empresas/{rutEmpresa}")]
        public IActionResult EliminarEmpresa(string rutEmpresa)
        {
            var empresaExistente = empresas.Find(c => c.RutEmpresa == rutEmpresa);
            if (empresaExistente == null)
            {
                return StatusCode(404);
            }
            empresas.Remove(empresaExistente);
            return NoContent();
        }
        //metodo calcular-Iva
        [HttpPost("calculariva")]
        public IActionResult CalcularIva([FromBody] Empresa cliente)
        {
            float montoIva = cliente.MontoVentas * 0.19f; // usando IVA de 19%.
            Empresa.MontoIVA = montoIva;
            return Ok(empresas);
        }
        //metodo calculador utilidades de la empresa
        [HttpPost("utilidades")]
        public IActionResult Utilidades([FromBody] Empresa cliente)
        {
            float montoIva = cliente.MontoVentas * 0.19f; // usando IVA de 19%.
            float montoUtilidades = cliente.MontoVentas - montoIva;
            cliente.MontoUtilidades = montoUtilidades;
            return Ok(cliente);
        }
    }



    
