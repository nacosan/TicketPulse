// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;

public class Ciudade
{
    public Id id { get; set; }
    public string idProvince { get; set; }
    public string name { get; set; }
    public string nameProvince { get; set; }
    public StateSky stateSky { get; set; }
    public Temperatures temperatures { get; set; }
}

public class Id
{
    [JsonProperty("0")]
    public string _0 { get; set; }
}

public class Origen
{
    public string productor { get; set; }
    public string web { get; set; }
    public string language { get; set; }
    public string copyright { get; set; }
    public string nota_legal { get; set; }
    public string descripcion { get; set; }
}

public class Provincia
{
    public string CODPROV { get; set; }
    public string NOMBRE_PROVINCIA { get; set; }
    public string CODAUTON { get; set; }
    public string COMUNIDAD_CIUDAD_AUTONOMA { get; set; }
    public string CAPITAL_PROVINCIA { get; set; }
}

public class Root
{
    public Origen origen { get; set; }
    public string elaborado { get; set; }
    public string title { get; set; }
    public List<Ciudade> ciudades { get; set; }
    public Today today { get; set; }
    public Tomorrow tomorrow { get; set; }
    public List<Provincia> provincias { get; set; }
    public List<object> breadcrumb { get; set; }
    public string metadescripcion { get; set; }
    public string keywords { get; set; }
}

public class StateSky
{
    public string description { get; set; }
    public string id { get; set; }
}

public class Temperatures
{
    public string max { get; set; }
    public string min { get; set; }
}

public class Today
{
    public List<string> p { get; set; }
}

public class Tomorrow
{
    public List<string> p { get; set; }
}

