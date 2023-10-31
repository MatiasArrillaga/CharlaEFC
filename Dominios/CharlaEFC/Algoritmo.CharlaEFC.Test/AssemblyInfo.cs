using System.Runtime.InteropServices;
using Xunit;

#region Configuracion Xunit.Extensions.Ordering

///
/// Esta configuración permite establecer un orden de ejecución de las clases de Test
/// 
//Optional
[assembly: CollectionBehavior(DisableTestParallelization = true)]
//Optional
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
//Optional
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

#endregion

// En proyectos de estilo SDK como este, varios atributos de ensamblado que definían
// en este archivo se agregan ahora automáticamente durante la compilación y se rellenan
// con valores definidos en las propiedades del proyecto. Para obtener detalles acerca
// de los atributos que se incluyen y cómo personalizar este proceso, consulte https://aka.ms/assembly-info-properties


// Al establecer ComVisible en false, se consigue que los tipos de este ensamblado
// no sean visibles para los componentes COM. Si tiene que acceder a un tipo en este
// ensamblado desde COM, establezca el atributo ComVisible en true en ese tipo.

[assembly: ComVisible(false)]

// El siguiente GUID es para el identificador de typelib, si este proyecto se expone
// en COM.

[assembly: Guid("edf5fa1b-0298-47c3-9001-cfcc7d50c2bd")]
