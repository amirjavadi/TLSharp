using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(149257707)]
    public class TLRequestSendChangePhoneCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 149257707;
            }
        }

                public int flags {get;set;}
        public bool allow_flashcall {get;set;}
        public string phone_number {get;set;}
        public bool? current_number {get;set;}
        public Auth.TLSentCode Response{ get; set;}


		public void ComputeFlags()
		{
			flags = 0;
flags = allow_flashcall ? (flags | 1) : (flags & ~1);
flags = current_number != null ? (flags | 1) : (flags & ~1);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
allow_flashcall = (flags & 1) != 0;
phone_number = StringUtil.Deserialize(br);
if ((flags & 1) != 0)
current_number = BoolUtil.Deserialize(br);
else
current_number = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

StringUtil.Serialize(phone_number,bw);
if ((flags & 1) != 0)
BoolUtil.Serialize(current_number.Value,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Auth.TLSentCode)ObjectUtils.DeserializeObject(br);

		}
    }
}
