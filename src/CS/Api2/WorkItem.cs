public class WorkItem
{
    public Guid Id { get; set; }
    public string ContentName { get; set; }
    public int? ContentLength { get; set; }
    public string BlobUrl { get; set; }
}