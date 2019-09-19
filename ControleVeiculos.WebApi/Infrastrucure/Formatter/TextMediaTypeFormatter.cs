using Lean.Test.Cloud.WebApi.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Lean.Test.Cloud.WebApi.Infrastrucure.Formatter
{
    public class TextMediaTypeFormatter : MediaTypeFormatter
    {
        public TextMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypeConst.TextPlain));
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            try
            {
                var ms = new MemoryStream();
                readStream.CopyTo(ms);
                var result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                taskCompletionSource.SetResult(result);

            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }

            return taskCompletionSource.Task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, System.Net.TransportContext transportContext, System.Threading.CancellationToken cancellationToken)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(value.ToString());
            return writeStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(string);
        }
    }
}