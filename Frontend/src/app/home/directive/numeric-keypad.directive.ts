import {
  Directive,
  ElementRef,
  HostListener,
  ComponentFactoryResolver,
  ComponentRef,
  Injector,
  ApplicationRef,
  EmbeddedViewRef,
} from '@angular/core';
import { NumericKeypadComponent } from '../numeric-keypad/numeric-keypad.component';

@Directive({
  selector: '[appNumericKeypad]',
})
export class NumericKeypadDirective {
  private keypadComponentRef: ComponentRef<NumericKeypadComponent>;
  private previousValue: string;

  constructor(
    private el: ElementRef,
    private resolver: ComponentFactoryResolver,
    private injector: Injector,
    private appRef: ApplicationRef
  ) {}

  @HostListener('focus') onFocus() {

    if (this.el.nativeElement.value.trim() === '0') {
      this.el.nativeElement.value = '';
    }
    this.previousValue = this.el.nativeElement.value;
    this.selectInputText();
    this.showKeypad();
  }

  @HostListener('click') onClick() {
    this.previousValue = this.el.nativeElement.value;
    this.showKeypad();
  }

  @HostListener('blur') onBlur() {
    setTimeout(() => this.hideKeypad(), 100); // Delay to prevent immediate hiding on clicking keypad
  }

  private selectInputText() {
    this.el.nativeElement.select();
  }
  private showKeypad() {
    if (!this.keypadComponentRef) {
      const factory = this.resolver.resolveComponentFactory(NumericKeypadComponent);
      this.keypadComponentRef = factory.create(this.injector);
      this.appRef.attachView(this.keypadComponentRef.hostView);
      const domElem = (this.keypadComponentRef.hostView as EmbeddedViewRef<any>).rootNodes[0] as HTMLElement;
      document.body.appendChild(domElem);

      this.keypadComponentRef.instance.numberPressed.subscribe((number: number) => this.appendNumber(number));
      this.keypadComponentRef.instance.backspacePressed.subscribe(() => this.backspace());
      // this.keypadComponentRef.instance.clearPressed.subscribe(() => this.clear());
      this.keypadComponentRef.instance.enterPressed.subscribe(() => {
        this.enter();
        this.hideKeypad();
      });
      this.keypadComponentRef.instance.escPressed.subscribe(() => {
        this.esc();
        this.hideKeypad();
      });
      this.keypadComponentRef.instance.increasePressed.subscribe(() => this.increase());
      this.keypadComponentRef.instance.decreasePressed.subscribe(() => this.decrease());
      this.keypadComponentRef.instance.decimalPressed.subscribe(() => this.appendDecimal());

      this.positionKeypad(domElem);
    }
  }

  private hideKeypad() {
    if (this.keypadComponentRef) {
      this.appRef.detachView(this.keypadComponentRef.hostView);
      this.keypadComponentRef.destroy();
      this.keypadComponentRef = null;
    }
  }

  private positionKeypad(domElem: HTMLElement) {
    const rect = this.el.nativeElement.getBoundingClientRect();
    domElem.style.position = 'absolute';
    domElem.style.top = `${rect.bottom + window.scrollY}px`;
    domElem.style.left = `${rect.left + window.scrollX}px`;
    domElem.style.zIndex = '1000'; // Ensure the keypad is on top
  }

  private appendNumber(number: number) {

    this.el.nativeElement.value += number;
    //only enter key set change value
    //this.el.nativeElement.dispatchEvent(new Event('input'));
  }
  private appendDecimal() {
    // Prevent multiple decimals
    if (!this.el.nativeElement.value.includes('.')) {

      this.el.nativeElement.value += '.';
      //only enter key set change value
      //this.el.nativeElement.dispatchEvent(new Event('input'));
    }

else{

  let value = parseInt(this.el.nativeElement.value, 10);
  console.log(value)

}

  }
  private backspace() {
    this.el.nativeElement.value = this.el.nativeElement.value.slice(0, -1);
    //only enter key set change value
    //this.el.nativeElement.dispatchEvent(new Event('input'));
  }

  // private clear() {
  //   this.el.nativeElement.value = '';
  //   this.el.nativeElement.dispatchEvent(new Event('input'));
  // }

  private enter() {
    // Trigger change event to notify Angular about the new value
    this.el.nativeElement.dispatchEvent(new Event('input', { bubbles: true }));
    this.el.nativeElement.dispatchEvent(new Event('change', { bubbles: true }));
    this.el.nativeElement.dispatchEvent(new KeyboardEvent('keydown', { key: 'Enter' }));
  }

  private esc() {
    // Revert to initial value
    this.el.nativeElement.value = this.previousValue || 0;
    this.el.nativeElement.dispatchEvent(new KeyboardEvent('keydown', { key: 'Escape' }));
  }

  private increase() {
    let value = parseInt(this.el.nativeElement.value, 10);
    if (!isNaN(value)) {
      value += 1;
      this.el.nativeElement.value = value.toString();
      //only enter key set change value
      //this.el.nativeElement.dispatchEvent(new Event('input'));
    }
    else{
      this.el.nativeElement.value = 0;
    }
  }

  private decrease() {
    let value = parseInt(this.el.nativeElement.value, 10);
    if (!isNaN(value)) {
      value -= 1;
      this.el.nativeElement.value = value.toString();
      //only enter key set change value
      //this.el.nativeElement.dispatchEvent(new Event('input'));
    }
    else{
      this.el.nativeElement.value = 0;
    }
  }
}
