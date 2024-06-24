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

  constructor(
    private el: ElementRef,
    private resolver: ComponentFactoryResolver,
    private injector: Injector,
    private appRef: ApplicationRef
  ) {}

  @HostListener('focus') onFocus() {
    this.showKeypad();
  }

  @HostListener('click') onClick() {
    this.showKeypad();
  }

  @HostListener('blur') onBlur() {
    setTimeout(() => this.hideKeypad(), 100); // Delay to prevent immediate hiding on clicking keypad
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
      this.keypadComponentRef.instance.clearPressed.subscribe(() => this.clear());

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
    this.el.nativeElement.dispatchEvent(new Event('input'));
  }

  private backspace() {
    this.el.nativeElement.value = this.el.nativeElement.value.slice(0, -1);
    this.el.nativeElement.dispatchEvent(new Event('input'));
  }

  private clear() {
    this.el.nativeElement.value = '';
    this.el.nativeElement.dispatchEvent(new Event('input'));
  }
}
