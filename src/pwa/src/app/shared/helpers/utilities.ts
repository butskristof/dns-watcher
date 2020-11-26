const utilities = {
  isNullOrWhitespace: (input: string) => {
    return !input || !input.trim();
  },
  addQueryParamToUrl: (url: string, key: string, value: string) => {
    const obj = new URL(url);
    obj.searchParams.append(key, value);
    return obj.toString();
  },
  // dateToString: (date: Date) => {
  //   const numberPipe = ServiceLocator.injector.get(DatePipe);
  //   return numberPipe.transform(date, 'yyyy-MM-dd');
  // },
  downloadFile: (url: string) => {
    window.location.href = url;
  }
};

export default utilities;
