using System;
using System.Linq;

namespace Nethereum.ABI.Decoders
{
    public class BytesTypeDecoder : TypeDecoder
    {
        private StringTypeDecoder stringTypeDecoder;

        public BytesTypeDecoder()
        {
            this.stringTypeDecoder = new StringTypeDecoder();    
        }
        public override bool IsSupportedType(Type type)
        {
            return type == typeof (string) || type == typeof (byte[]) || type == typeof(object);
        }

        public override object Decode(byte[] encoded, Type type)
        {
            if (!IsSupportedType(type)) throw new NotSupportedException(type + " is not supported");
            if (type == typeof (string)) return stringTypeDecoder.Decode(encoded, type);
            return encoded.Skip(32).Take(EncoderDecoderHelpers.GetNumberOfBytes(encoded)).ToArray();
        }

        public override Type GetDefaultDecodingType()
        {
            return typeof (byte[]);
        }
    }
}