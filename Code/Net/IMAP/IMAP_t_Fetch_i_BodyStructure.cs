﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Merculia.Net.IMAP
{
    /// <summary>
    /// This class represents FETCH request BODYSTRUCTURE argument(data-item). Defined in RFC 3501.
    /// </summary>
    public class IMAP_t_Fetch_i_BodyStructure : IMAP_t_Fetch_i
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public IMAP_t_Fetch_i_BodyStructure()
        {
        }


        #region override method ToString

        /// <summary>
        /// Returns this as string.
        /// </summary>
        /// <returns>Returns this as string.</returns>
        public override string ToString()
        {
            return "BODYSTRUCTURE";
        }

        #endregion
    }
}
