/*
** Implementation of client side Validation  () for Proposal
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
*/
import { FormikErrors } from 'formik';
import { IProposalData } from './generated/ProposalTypes';

export class ProposalValidator
{
    static Validate = async (values: IProposalData) : Promise<FormikErrors<IProposalData>> => {
        var errors: FormikErrors<IProposalData> = {};

        // add validations here
        // add any error messages on the error object:
        // errors.PropertyName = "error message"
        if((!!values.endDate && !!values.startDate) && (values.endDate < values.startDate))
        {
            errors.endDate = "End date must be after start date";
        }

        return errors;
    };
}

