# SqlBulkCopy.Utilities #

El motivo principal de la creación de esta librería no es otro que un wrapper de ***System.Data.SqlClient.SqlBulkCopy.WriteToServer*** con el fin de evitar entre otras cosas que si cambiamos el orden de las propiedades de nuestras entidades no afecten a dicho método.

Expone una clase no estática (***SqlBulkUtilities***) con tres métodos públicos.  

    public void WriteToServer(SortedDataTable sortedDataTable)
    public SortedDataTable GetSortedDataTable<T>(string tableName, IEnumerable<T> items) where T : IContextEntity
    public SortedDataTable GetSortedDataTable<T>(DbContext context, IEnumerable<T> items) where T : IContextEntity

void **WriteToServer**(*SortedDataTable sortedDataTable*)

----------

- Es el método que realiza la llamada al método WriteToServer de SqlBulkCopy.
- Tiene un parámetro del tipo SortedDataTable

SortedDataTable **GetSortedDataTable**<T>(*string tableName, IEnumerable<T> items*) where T : IContextEntity

----------

- Es el método que obtiene el tipo SortedDataTable pasándole como parámetro el nombre de la tabla de destino así como un Enumerable de elementos que implementen la interface IContextEntity.

SortedDataTable **GetSortedDataTable**<T>(*DbContext context, IEnumerable<T> items*) where T : IContextEntity

----------

- Es el método que obtiene el tipo SortedDataTable pasándole como parámetro el contexto de Entity Framework así como un Enumerable de elementos que implementen la interface IContextEntity.

## Observaciones ##
- La interface IContextEntity es una interface vacía. Entiendo que no es una buena práctica.

