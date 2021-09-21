/*
** Implementation of client side Validation  () for Notification
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
*/
import { FormikErrors } from 'formik';
import { INotificationData } from './generated/NotificationTypes';

export class NotificationValidator
{
    static Validate = async (values: INotificationData) : Promise<FormikErrors<INotificationData>> => {
        var errors: FormikErrors<INotificationData> = {};

        // add validations here
        // add any error messages on the error object:
        // errors.PropertyName = "error message"

        return errors;
    };
}

