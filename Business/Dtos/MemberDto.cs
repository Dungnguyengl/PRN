namespace ViewModel.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MemberDto dto &&
                   Id == dto.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
