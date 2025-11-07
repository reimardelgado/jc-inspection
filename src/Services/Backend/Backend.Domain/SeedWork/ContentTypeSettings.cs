using System.Collections;

namespace Backend.Domain.SeedWork
{
    public static class ContentTypeSettings
    {
        public static Hashtable ContentTypesToFile => new Hashtable
        {
            {"jpg", "image/jpg"},
            {"jpeg", "image/jpg"},
            {"png", "image/png"},
            {"pdf", "application/pdf"},
            {"xls", "application/vnd.ms-excel"},
            {"xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {"docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"doc", "application/msword"},
            {"csv", "text/csv"},
            {"txt", "text/plain"},
            {"tsv", "text/tsv"}
        };

        public static Hashtable FileToContentTypes => new Hashtable
        {
            {"image/jpg", "jpg"},
            {"image/jpeg", "jpg"},
            {"image/png", "png"},
            {"application/pdf", "pdf"},
            {"application/vnd.ms-excel", "xls"},
            {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "xlsx"},
            {"application/vnd.openxmlformats-officedocument.wordprocessingml.document","docx"},
            {"application/msword", "doc"},
            {"text/csv", "csv"},
            {"text/plain", "txt"},
            {"text/tsv", "tsv"},
        };
    }
}