using System;
namespace Services.DTO
{
    public class ResultDTO<T>
    {
        public ResultDTO(T dto)
        {
            this.Data = dto;
        }

        public T Data { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}

