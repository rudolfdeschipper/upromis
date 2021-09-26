/*
** Implementation of client side Validation for Payment
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
*/
import { FormikErrors } from 'formik';
import { IPayments } from './generated/ProposalTypes';

export class PaymentValidator
{
    static Validate = async (values: IPayments) : Promise<FormikErrors<IPayments>> => {
        var errors: FormikErrors<IPayments> = {};

        // add validations here
        // add any error messages on the error object:
        // errors.PropertyName = "error message"

        return errors;
    };
}
