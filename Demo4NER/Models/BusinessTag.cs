namespace Demo4NER.Models
{
    public class BusinessTag
    {
        // Para gerar a tabela intermédia. EFCore ainda n faz automaticamente
        public int Id { get; set; }
        public Business Business { get; set; }
        public Tag Tag { get; set; }
    }
}