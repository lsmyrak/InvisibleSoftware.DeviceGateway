namespace InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos
{
    public class LookupResponse<T>
    {
        public List<LookupColumn> Columns { get; set; } = new List<LookupColumn>();
        public List<T> Data { get; set; } = new List<T>();
    }

    public class LookupColumn
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public bool Display { get; set; } = true;
    }
}