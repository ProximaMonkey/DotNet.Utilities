﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace YanZhiwei.DotNet.AuthWebApiExample.Filters
{
    public class CompressContentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var acceptedEncoding = context.Response.RequestMessage.Headers.AcceptEncoding.First().Value;
            
            if(!acceptedEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase)
                    && !acceptedEncoding.Equals("deflate", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }
            
            context.Response.Content = new CompressContent(context.Response.Content, acceptedEncoding);
        }
    }
    
    public class CompressContent : HttpContent
    {
        private readonly string _encodingType;
        private readonly HttpContent _originalContent;
        
        public CompressContent(HttpContent content, string encodingType = "gzip")
        {
            _originalContent = content;
            _encodingType = encodingType.ToLowerInvariant();
            Headers.ContentEncoding.Add(encodingType);
        }
        
        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
        
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Stream compressStream = null;
            
            switch(_encodingType)
            {
                case "gzip":
                    compressStream = new GZipStream(stream, CompressionMode.Compress, true);
                    break;
                    
                case "deflate":
                    compressStream = new DeflateStream(stream, CompressionMode.Compress, true);
                    break;
                    
                default:
                    compressStream = stream;
                    break;
            }
            
            return _originalContent.CopyToAsync(compressStream).ContinueWith(tsk =>
            {
                if(compressStream != null)
                {
                    compressStream.Dispose();
                }
            });
        }
    }
}