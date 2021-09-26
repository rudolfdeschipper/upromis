/*
** Implementation of client side Validation for Activity
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
*/
import { FormikErrors } from 'formik';
import { IActivities } from './generated/WorkpackageTypes';

export class ActivityValidator
{
    static Validate = async (values: IActivities) : Promise<FormikErrors<IActivities>> => {
        var errors: FormikErrors<IActivities> = {};

        // add validations here
        // add any error messages on the error object:
        // errors.PropertyName = "error message"

        return errors;
    };
}
