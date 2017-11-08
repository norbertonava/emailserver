﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Merculia.Net.IMAP.Client;

namespace Merculia.Net.IMAP
{
    /// <summary>
    /// This class represents IMAP SEARCH <b>RECENT</b> key. Defined in RFC 3501 6.4.4.
    /// </summary>
    /// <remarks>Messages that have the \Recent flag set.</remarks>
    public class IMAP_Search_Key_Recent : IMAP_Search_Key
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public IMAP_Search_Key_Recent()
        {
        }


        #region static method Parse

        /// <summary>
        /// Returns parsed IMAP SEARCH <b>RECENT</b> key.
        /// </summary>
        /// <param name="r">String reader.</param>
        /// <returns>Returns parsed IMAP SEARCH <b>RECENT</b> key.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <b>r</b> is null reference.</exception>
        /// <exception cref="ParseException">Is raised when parsing fails.</exception>
        internal static IMAP_Search_Key_Recent Parse(StringReader r)
        {
            if(r == null){
                throw new ArgumentNullException("r");
            }

            string word = r.ReadWord();
            if(!string.Equals(word,"RECENT",StringComparison.InvariantCultureIgnoreCase)){
                throw new ParseException("Parse error: Not a SEARCH 'RECENT' key.");
            }

            return new IMAP_Search_Key_Recent();
        }

        #endregion


        #region override method ToString

        /// <summary>
        /// Returns this as string.
        /// </summary>
        /// <returns>Returns this as string.</returns>
        public override string ToString()
        {
            return "RECENT";
        }

        #endregion


        #region internal override method ToCmdParts

        /// <summary>
        /// Stores IMAP search-key command parts to the specified array.
        /// </summary>
        /// <param name="list">Array where to store command parts.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>list</b> is null reference.</exception>
        internal override void ToCmdParts(List<IMAP_Client_CmdPart> list)
        {
            if(list == null){
                throw new ArgumentNullException("list");
            }

            list.Add(new IMAP_Client_CmdPart(IMAP_Client_CmdPart_Type.Constant,ToString()));
        }

        #endregion
    }
}
