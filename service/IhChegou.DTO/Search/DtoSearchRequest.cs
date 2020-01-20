namespace IhChegou.DTO.Search
{
    public class DtoSearchRequest
    {
        public string Query { get; set; }
        public int From { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}