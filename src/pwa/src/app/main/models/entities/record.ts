import {RecordType} from './record-type';

export class Record {
  id?: string;
  modifiedOn?: Date;

  prefix?: string;
  expectedValue?: string;
  expectedTimeToLive?: string;
  recordType?: RecordType;

  constructor(record: Record | null = null) {
    if (record) {
      Object.assign(this, record);
    }
  }
}
