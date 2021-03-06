/*
** Implementation of action Copy (copy) for Contract
** This file is only generated once; you may change it as needed.
** In case you want to re-generate it, simply delete it an re-run the code generator.
** Do not change the method's name or signature as this wil break the calling code.
**
*/
import { ILoadResult, ISaveMessage, IAPIResult } from '../GeneralTypes';
import { http } from '../http';
import { Utils } from '../Utils';
import { IContractData } from './generated/ContractTypes';
import {ContractAPI} from './generated/ContractAPI';

export class ContractActioncopy {
    static perform = async (id: number, token: string): Promise<ISaveMessage<IContractData>> => {
        try {
            // get the record from the backend
            var recAPIResult = await ContractAPI.loadOneRecord(id, token);
            var rec : IContractData = recAPIResult.dataSubject!;

            // perform action on the record
            // TODO
            // send the updated record back to the caller for saving
            // create copy
            rec = {...rec, id:0, modifier:"Added"};
            // reset children:
            rec.Payments?.forEach(p => {p.id = 0; p.modifier = "Added"} );
            rec.Teammembers?.forEach(p => {p.id = 0; p.modifier = "Added"} );

            var saveMessage : ISaveMessage<IContractData> =  { id: 0, dataSubject : rec, action: "Navigate", subaction: "/contractdetails/add", additionalData: [] };
            return saveMessage;
        } catch (ex) {
            console.error(ex);
            throw ex;
        }
    }
}
