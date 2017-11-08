using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Merculia.MailServer.Filters
{
    /// <summary>
    /// TODO: help ! don't know how to commant it.
    /// </summary>
    public interface ISettingsUI
    {
        #region method GetUI

        /// <summary>
        /// Gets settings UI window.
        /// </summary>
        /// <returns></returns>
        Form GetUI();

        #endregion
    }
}
