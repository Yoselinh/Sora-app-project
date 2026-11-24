namespace Sora_AppProject.Application.Dtos.Common;

public class 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    PaginatedDto<T>
{
    public IReadOnlyList<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages
    {
        get
        {
            if (PageSize <= 0)
            {
                return 0;
            }
            return (int)Math.Ceiling(TotalCount / (double)PageSize);
        }
    }
}
