/*
** Implementation of client side Validation  () for UserManager
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
*/
import { FormikErrors } from 'formik';
import { IUserManagerData } from './generated/UserManagerTypes';

export class UserManagerValidator
{
    static Validate = async (values: IUserManagerData) : Promise<FormikErrors<IUserManagerData>> => {
        var errors: FormikErrors<IUserManagerData> = {};

        // add validations here
        // add any error messages on the error object:
        // errors.PropertyName = "error message"

        return errors;
    };
}

