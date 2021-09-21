/*
** Implementation of client side Validation  () for Client
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
*/
import { FormikErrors } from 'formik';
import { IClientData } from './generated/ClientTypes';

export class ClientValidator
{
    static Validate = async (values: IClientData) : Promise<FormikErrors<IClientData>> => {
        var errors: FormikErrors<IClientData> = {};

        // add validations here
        // add any error messages on the error object:
        // errors.PropertyName = "error message"

        return errors;
    };
}

