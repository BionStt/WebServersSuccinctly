using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clifton.WebServer
{
	public abstract class Response
	{
		private byte[] data;

		public virtual byte[] Data
		{
			get { return data; }
			set { data = value; }
		}

		public virtual Encoding Encoding { get { return Encoding.UTF8; } }
		public virtual string MimeType { get; set; }
	}

	/// <summary>
	/// A pending byte response packet for non-text (as in binary) data like images.
	/// </summary>
	public class PendingByteResponse : Response
	{
	}

	/// <summary>
	/// A pending HTML page response packet.
	/// </summary>
	public class PendingPageResponse : Response
	{
		public string Html { get; set; }
		public override byte[] Data { get { return Encoding.UTF8.GetBytes(Html); } }
	}

	/// <summary>
	/// A pending file response (like a Javascript or CSS file, which is still text.)
	/// </summary>
	public class PendingFileResponse : Response
	{
	}

	/// <summary>
	/// A wrapper for HttpListenerContext so we can put pending
	/// byte[] and HTML responses into a workflow for downstream final
	/// processing (basically only for HTML) by a view engine.
	/// </summary>
	public class ContextWrapper
	{
		public HttpListenerContext Context { get; protected set; }
		public Response PendingResponse { get; set; }

		public ContextWrapper(HttpListenerContext context)
		{
			Context = context;
		}
	}
}
