using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils.Constants
{
    public class Messages
    {
        #region ApiResponse
        public static readonly string GetedData = "Datos obtenidos";
        public static readonly string PostedData = "Datos posteados";
        public static readonly string PatchedData = "Datos acualizados";
        public static readonly string DeletedData = "Datos eliminados";
        public static readonly string AccionSucceed = "Acción exitosa";
        #endregion

        #region ValidationPostUsers

        public static readonly string ExistingUserName = "El nombre de usuario ya existe.";
        public static readonly string ExistingMail = "El correo electrónico ya esta siendo usado por otro usaurio.";
        public static readonly string FormatPasswordNotMatch = "La constraseña debe contar con minimo con 8 caracteres, al menos unos letra mayúscula, una letra minúscula, un número y alguno de los siguietes caracteres especiales: (@$!%*?&).";
        public static readonly string FormatEmailNotMatch = "El correo no tiene un formato correcto.";
        public static readonly string ItsSamePassword = "La contraseña no puede ser igual a la actual.";

        #endregion

        #region ChangePassword
        public static readonly string PasswordChanged = "La contraseña se actualizo correctamente.";
        #endregion

        #region Authenticate 

        public static readonly string UserDontExist = "El nombre de usuario no existe en el sistema.";
        public static readonly string ErrorAuthenticate = "Nombre de usuario o contraseña incorrecta.";
        public static readonly string InvalidUser = "Invalid User";
        public static readonly string TokenExpired = "Token Expired";
        public static readonly string InvalidToken = "Invalid Token";

        #endregion

        #region Brand

        public const string BrandUsed = "No es posible eliminar la marca, esta asociada a un produto activo";

        #endregion

        #region Bank
        public const string BankUsedAccount = "No es posible eliminar el banco, esta asociada a una cuenta bancaria";
        #endregion

        #region Category

        public const string CategoryUsed = "No es posible eliminar la categoria, esta asociada a un producto activo";

        #endregion

        #region Presentation

        public const string PresentationUsed = "No es posible eliminar la presentación, esta asociada a un producto activo";

        #endregion

        #region SubCategory

        public const string SubCategoryUsed = "No es posible eliminar la subcategoria, esta asociada a un producto activo";

        #endregion

        #region Telephony
        public const string TelephonyUsedPhone = "No es posible eliminar la telefonia, esta asociada a un teléfono";
        #endregion

        #region Product

        public const string SKUDontAvalible = "El SKU ya esta siendo utilizado por otro producto.";
        public const string VendorProductUsed = "No es posible eliminar el producto, esta asociado a un proveedor activo";

        #endregion

        #region UnitOfMeasure
        public const string UnitOfMeasureUsed = "No es posible eliminar la unidad de medida, esta asociada a un produto activo";
        #endregion

        #region Vendor
        public const string ProductUseVendorDelete = "No es posible eliminar al proveedor, ya que tiene productos asociados";
        #endregion
    }
}
