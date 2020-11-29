import {RecordType} from '../../entities/domains/record-type';
import {Record} from '../../entities/domains/record';

export class CreateOrUpdateRecordData {
  recordType: RecordType = RecordType.A;
  prefix?: string;
  expectedValue?: string;
  expectedTimeToLive = 3600;

  id?: string;
  modifiedOn?: Date;

  constructor(record?: Record) {
    if (record) {
      Object.assign(this, record);
    }
  }
}
