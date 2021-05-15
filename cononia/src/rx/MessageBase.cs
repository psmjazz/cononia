using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.rx
{
    // 노드간 이동할 데이터 베이스 클래스
    public class MessageBase
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return "base : " + Message;
        }
    }
}
